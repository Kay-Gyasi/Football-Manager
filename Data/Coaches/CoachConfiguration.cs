using Data.Base;

namespace Data.Coaches;

public class CoachConfiguration : DatabaseConfiguration<Coach, int>
{
    public override void Configure(EntityTypeBuilder<Coach> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.IsMain).HasDefaultValue(true);
    }
}