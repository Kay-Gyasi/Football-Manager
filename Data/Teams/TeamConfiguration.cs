namespace Data.Teams;

public class TeamConfiguration : DatabaseConfiguration<Team, int>
{
    public override void Configure(EntityTypeBuilder<Team> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50);
        builder.Property(x => x.StadiumName)
            .HasColumnType("varchar")
            .HasMaxLength(50);
    }
}