using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIRS.Domain.Models;

public class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
{
    public void Configure(EntityTypeBuilder<IssueType> builder)
    {
        builder.Property(x => x.Type)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);
    }
}
