using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(x => x.Street)
               .HasMaxLength(200);

        builder.Property(x => x.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(x => x.issueType)
               .WithMany()
               .HasForeignKey(x => x.IssueTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Governorate)
               .WithMany()
               .HasForeignKey(x => x.GovernorateId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Municipality)
               .WithMany()
               .HasForeignKey(x => x.MunicipalityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
               .WithMany(x => x.CreatedIssues)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AssignedEmployee)
               .WithMany(x => x.AssignedIssues)
               .HasForeignKey(x => x.AssignedEmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
