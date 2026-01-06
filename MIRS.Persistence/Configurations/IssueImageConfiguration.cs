using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class IssueImageConfiguration : IEntityTypeConfiguration<IssueImage>
{
    public void Configure(EntityTypeBuilder<IssueImage> builder)
    {
        builder.Property(x => x.ImagePath)
               .IsRequired();

        builder.Property(x => x.ImageType)
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne(x => x.Issue)
               .WithMany(x => x.Images)
               .HasForeignKey(x => x.IssueId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UploadedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
