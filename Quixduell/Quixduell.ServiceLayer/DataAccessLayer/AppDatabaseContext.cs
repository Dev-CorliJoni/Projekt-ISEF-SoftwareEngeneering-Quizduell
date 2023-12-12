using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.Blazor.Data
{
    public class AppDatabaseContext<TUser> 
        : IdentityDbContext<TUser, IdentityRole, string>
        where TUser : User
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext<User>> options)
            : base(options)
        {
        }

        public DbSet<Studyset> Studysets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<MultipleChoiceQuestion>();
                .HasDiscriminator<string>("answer_type")
                .HasValue<Answer>("a")
                .HasValue<MultipleChoiceAnswer>("mca");

            builder.Entity<BaseQuestion>()
                .HasDiscriminator<string>("question_type")
                .HasValue<OpenQuestion>("oq");
        }
    }
}