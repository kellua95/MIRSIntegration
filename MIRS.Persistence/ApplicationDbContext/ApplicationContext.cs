using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MIRS.Domain.Models;

namespace MIRS.Persistence.ApplicationDbContext
{
    public class ApplicationContext
        : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }


        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<IssueImage> IssueImages { get; set; }
        public DbSet<IssueComment> IssueComments { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
            );

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var decimalProperties = entityType.ClrType
                        .GetProperties()
                        .Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in decimalProperties)
                    {
                        modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasConversion<double>();
                    }
                }
            }
        }
    }
}
