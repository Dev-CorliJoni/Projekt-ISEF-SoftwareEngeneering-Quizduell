﻿using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace Quixduell.Blazor.EditFormModel
{
    public class CreateEditStudysetFormModel
    {
        public Guid ID { get; set; }

        [StringLength(150, MinimumLength = 5, ErrorMessage = "Der Name muss eine Länge zwischen {1} und {2} haben")]
        public string Name { get; set; } = String.Empty;
        [Required]
        public Category? Category { get; set; }
        public List<User>  Contributors { get; set; } = new List<User>();
        public List<UserStudysetConnection> UserStudysetConnections { get; set; } = new List<UserStudysetConnection>();
        [Required]
        public User Creator { get; set; }

        [MinLength(1)]
        public List<CreateEditQuestionFormModel> QuestionFormModels { get; set; } = new List<CreateEditQuestionFormModel>();


        public CreateEditStudysetFormModel(User creator)
        {
            Creator = creator;
        }
        public CreateEditStudysetFormModel(Studyset studyset)
        {
            ID = studyset.Id;
            Name = studyset.Name;
            Contributors = studyset.Contributors;
            Creator = studyset.Creator;
            Category = studyset.Category;
            UserStudysetConnections = studyset.Connections;
            if (studyset.Questions is not null) 
            {
                foreach(var question in studyset.Questions)
                {
                    QuestionFormModels.Add(new(question));
                }
            }
        }
    }
}
