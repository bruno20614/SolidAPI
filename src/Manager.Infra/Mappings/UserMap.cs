using Manager.Domain.entities
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        EntityTypeBuilderToTable("User");

        builder.hasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UserIdentityColumn()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Name)
            .Isrequired()
            .HasMaxLength(30)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("password")
            .HasColumnType("VARCHAR(180)");

        builder.Property(x =>x.Email)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnName("email")
            .HasColumnType("VARCHAR(180)")
    }       
}