using Data.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Players;

public class PlayerConfiguration : DatabaseConfiguration<Player, int>
{
    public override void Configure(EntityTypeBuilder<Player> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Nationality)
            .HasConversion(new EnumToStringConverter<Nationality>());
        builder.Property(x => x.PrimaryPosition)
            .HasConversion(new EnumToStringConverter<Position>());
        builder.Property(x => x.SecondaryPosition)
            .HasConversion(new EnumToStringConverter<Position>());
        builder.Property(x => x.JerseyName)
            .HasColumnType("varchar")
            .HasMaxLength(50);
    }
}