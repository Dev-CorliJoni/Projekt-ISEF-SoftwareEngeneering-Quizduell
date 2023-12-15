using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Questions;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class InitSampleData
    {
        private StudysetDataAccess _studysetData = null;
        private CategoryDataAccess _categoryData = null;

        public InitSampleData(StudysetDataAccess studysetData, CategoryDataAccess categoryData) 
        {
            _studysetData = studysetData;
            _categoryData = categoryData;
        }

        public async Task GenerateSampleData (User user)
        {
            Category category = new Category("Category1");

            var questions = new List<BaseQuestion>
            {
                new OpenQuestion("Werrrrr bist du?", "Du bist Du", new Answer($"Du bist {user.UserName}")),
                new OpenQuestion("Was ist deine Id?", "Kein Plan", new Answer($"Deine Id ist {user.Id}")),
                new OpenQuestion("Was ist deine Email?", "Kein Plan", new Answer($"Deine Email ist {user.Email}")),
                new MultipleChoiceQuestion("Um was geht es bei dem Projekt ISEF?", "EIn Uni Projekt", [
                    new MultipleChoiceAnswer("Ein Projekt das aus Spaß gemacht wird", false),
                    new MultipleChoiceAnswer("Ein Projekt mit dem Millionen generiert werden", false),
                    new MultipleChoiceAnswer("Ein Projekt das wir als Modul machen müssen", true)
                    ]
                )
            };

            List<Studyset> studysets = [
                new Studyset("Studyset1", category, user, new List<User>(), questions)
            ];

            if (await _categoryData.ExistsAsync(category.Name)== false ) 
            {
                await _categoryData.AddAsync(category);
            }

            foreach (var studyset in studysets)
            {
                if (await _studysetData.ExistsAsync(studyset.Name) == false)
                {
                    await _studysetData.AddAsync(studyset);
                }
            }            
        }

    }
}
