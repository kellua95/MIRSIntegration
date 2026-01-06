using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class IssueCommentConfiguration : IEntityTypeConfiguration<IssueComment>
{
    public void Configure(EntityTypeBuilder<IssueComment> builder)
    {
        builder.Property(x => x.Comment)
               .IsRequired();

        builder.Property(x => x.IsVisibleToUser)
               .HasDefaultValue(true);

        builder.HasOne(x => x.Issue)
               .WithMany(x => x.Comments)
               .HasForeignKey(x => x.IssueId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.CreatedByEmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
