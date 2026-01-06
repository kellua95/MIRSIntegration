using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasMany(x => x.Municipalities)
               .WithOne(x => x.Governorate)
               .HasForeignKey(x => x.GovernorateId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
