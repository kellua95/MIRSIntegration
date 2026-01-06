using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.FullName)
               .HasMaxLength(200);

        builder.HasMany(x => x.CreatedIssues)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.AssignedIssues)
               .WithOne(x => x.AssignedEmployee)
               .HasForeignKey(x => x.AssignedEmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
