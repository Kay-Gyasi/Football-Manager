using Data.Exceptions;

namespace Football_Manager.Coaches;

[Processor]
public class CoachProcessor
{
    private readonly ICoachRepository _coachRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<CoachProcessor> _logger;

    public CoachProcessor(ICoachRepository coachRepository,
        UserManager<User> userManager, ILogger<CoachProcessor> logger)
    {
        _coachRepository = coachRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<OneOf<int, InvalidIdException, InvalidDataException>> UpsertAsync(CoachCommand command)
    {
        var isNew = command.Id is null or 0;

        if (isNew)
        {
            return await SaveNew(command);
        }

        var coach = await _coachRepository.FindByIdAsync(command.Id ?? 0);
        if (coach is null) return new InvalidIdException();
        AssignFields(command, coach);
        await _coachRepository.UpdateAsync(coach, true);
        return coach.Id;
    }
    
    public async Task<CoachDto?> GetAsync(int id)
    {
        return (CoachDto) await _coachRepository.FindByIdAsync(id);
    }
    
    public async Task<CoachDto?> GetByType(int typeId)
    {
        return (CoachDto) await _coachRepository.GetByTypeId(typeId);
    }

    public async Task<PaginatedList<CoachPageDto>> GetPageAsync(PaginatedCommand query)
    {
        var paginatedList = await _coachRepository.GetPageAsync(query);
        return CoachPageDto.ToPageDto(paginatedList);
    }

    public async Task DeleteAsync(int id)
    {
        var coach = await _coachRepository.FindByIdAsync(id);
        if (coach is not null) 
            await _coachRepository.SoftDeleteAsync(coach, true);
    }
    
    private async Task<OneOf<int, InvalidIdException, InvalidDataException>> SaveNew(CoachCommand command)
    {
        await using var transaction = await _coachRepository.BeginTransaction();
        try
        {
            var user = User.Create(UserType.Coach, command.User?.FirstName ?? "", command.User?.LastName ?? "")
                .WithEmail(command.User?.Email)
                .WithPhone(command.User?.PhoneNumber)
                .WasBornOn(command.User?.DateOfBirth);
            
            var result = await _userManager.CreateAsync(user, command.User?.Password ?? "");
            if (result.Errors.Any())
            {
                _logger.LogError("{Errors}", result.Errors.ToString());
                return new InvalidDataException("Error while creating user");
            }

            await _userManager.AddToRoleAsync(user, UserType.Coach.ToString());
            
            var coach = Coach.Create(user.Id);
            AssignFields(command, coach);
            await _coachRepository.AddAsync(coach, true);
            await transaction.CommitAsync();
            return coach.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError("{Message}", e.Message);
            return new InvalidDataException(e.Message);
        }
    }

    private static void AssignFields(CoachCommand command, Coach coach)
    {
        coach.CoachesTeamWithId(command.TeamId)
            .IsMainCoach()
            .WithExperienceInYears(command.YearsOfExperience);
    }
}