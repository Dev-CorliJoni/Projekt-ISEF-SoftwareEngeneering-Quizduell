using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;

namespace Quixduell.ServiceLayer.DataAccessLayer
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

        public DbSet<OpenQuestion> OpenQuestions { get; set; }

        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Answer>()
                .HasDiscriminator<string>("answer_type")
                .HasValue<Answer>("a")
                .HasValue<MultipleChoiceAnswer>("mca");

            builder.Entity<BaseQuestion>()
                .HasDiscriminator<string>("question_type")
                .HasValue<MultipleChoiceQuestion>("mcq")
                .HasValue<OpenQuestion>("oq");



            builder.Entity<Studyset>()
             .HasMany<UserStudysetConnection>(st => st.Connections)
             .WithOne(usc => usc.Studyset)
             .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Answer>();
            builder.Entity<MultipleChoiceAnswer>();

            builder.Entity<MultipleChoiceQuestion>();
            builder.Entity<OpenQuestion>();

        }
    }
}