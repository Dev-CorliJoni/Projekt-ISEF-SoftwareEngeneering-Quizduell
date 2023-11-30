using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Data
{
    public class AppDatabaseContext<TUSer> 
        : IdentityDbContext<TUSer, IdentityRole, string>
        where TUSer : AppUser
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext<AppUser>> options)
            : base(options)
        {
        }

        public DbSet<Lernset> Lernsets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<AppUser>()
                .HasMany(a => a.CreatedLernsets)
                .WithOne(e => e.Creator)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<AppUser>()
                .HasMany(a => a.LernsetPermissions)
                .WithMany(l => l.Contributors)
                .UsingEntity<Dictionary<string, object>>("Relations_Contributors_LernsetPermissions",
                x => x.HasOne<Lernset>().WithMany().OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne<AppUser>().WithMany().OnDelete(DeleteBehavior.Cascade));
        }

       


    }
}