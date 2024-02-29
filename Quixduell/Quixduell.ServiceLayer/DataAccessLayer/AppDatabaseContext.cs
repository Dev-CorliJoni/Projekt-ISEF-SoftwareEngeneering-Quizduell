using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using System.Reflection.Emit;

namespace Quixduell.ServiceLayer.DataAccessLayer
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    public class AppDatabaseContext<TUser>
        : IdentityDbContext<TUser, IdentityRole, string>
        where TUser : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDatabaseContext{TUser}"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext<User>> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the study sets DbSet.
        /// </summary>
        public DbSet<Studyset> Studysets { get; set; }

        /// <summary>
        /// Gets or sets the categories DbSet.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the open questions DbSet.
        /// </summary>
        public DbSet<OpenQuestion> OpenQuestions { get; set; }

        /// <summary>
        /// Gets or sets the multiple choice questions DbSet.
        /// </summary>
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        /// <summary>
        /// Configures the database context with the specified model builder.
        /// </summary>
        /// <param name="builder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.StudysetConnections)
                .WithOne(connection => connection.User)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(u => u.CreatedStudysets)
                .WithOne(study => study.Creator)
                .OnDelete(DeleteBehavior.NoAction);



            builder.Entity<User>()
                .HasMany(u => u.ContributedStudysets)
                .WithMany(study => study.Contributors)
                 .UsingEntity<Dictionary<string, object>>(
                    "StudysetContributors",
                    u => u
                    .HasOne<Studyset>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction),
                    u => u
                    .HasOne<User>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                );


            builder.Entity<Answer>()
                .HasDiscriminator<string>("answer_type")
                .HasValue<Answer>("a")
                .HasValue<MultipleChoiceAnswer>("mca");

            builder.Entity<BaseQuestion>()
                .HasDiscriminator<string>("question_type")
                .HasValue<MultipleChoiceQuestion>("mcq")
                .HasValue<OpenQuestion>("oq");



            builder.Entity<Studyset>()
             .HasMany(st => st.Connections)
             .WithOne(usc => usc.Studyset)
             .OnDelete(DeleteBehavior.NoAction);
             



            builder.Entity<Studyset>()
             .HasMany(st => st.UsersRequestedToBecomeContributor)
             .WithMany(usc => usc.UsersRequestedToBecomeContributors)
             .UsingEntity<Dictionary<string, object>>(
                "UsersRequestedToBecomeContributor",
                u => u
                .HasOne<User>()
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction),
                u => u
                .HasOne<Studyset>()
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
            );





            builder.Entity<Answer>();
            builder.Entity<MultipleChoiceAnswer>();

            builder.Entity<MultipleChoiceQuestion>();
            builder.Entity<OpenQuestion>();
        }
    }
}
