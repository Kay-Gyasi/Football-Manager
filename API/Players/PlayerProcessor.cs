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
        await _playerRepository.UpdateAsync(player, true);
        return player.Id;
    }

    public async Task<PlayerDto?> GetAsync(int id)
    {
        var player = await _playerRepository.FindByIdAsync(id);
        player?.ForUser(await Task.Run(() => _userManager.Users.FirstOrDefault(x => x.Id == id)));
        return (PlayerDto) player;
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
                .WasBornOn(command.User?.DateOfBirth);
            var result = await _userManager.CreateAsync(user);
            if (result.Errors.Any())
            {
                _logger.LogError("{Errors}", result.Errors.ToString());
                return new InvalidDataException();
            }

            var password = _userManager.PasswordHasher.HashPassword(user, command.User.Password ?? "");
            await _userManager.AddPasswordAsync(user, password);
            await _userManager.AddToRoleAsync(user, UserType.Player.ToString());
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
            .HasSecondaryPosition(command.SecondaryPosition);
    }
}