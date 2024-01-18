using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity;

namespace Quixduell.ServiceLayer.ServiceLayer
{
    public class StudysetView
    {
        private readonly StudysetDataAccess _studysetDataAccess;
        private readonly CategoryDataAccess _categoryDataAccess;

        public StudysetView(StudysetDataAccess studysetDataAccess, CategoryDataAccess categoryDataAccess)
        {
            _studysetDataAccess = studysetDataAccess;
            _categoryDataAccess = categoryDataAccess;
        }

        public async Task StarStudysetAsync(Studyset studyset, UserStudysetConnection? connection, User user)
        {
            await Task.Run(() =>
            {
                connection = connection == null ? studyset.Connections.Find((sc) => sc.User == user) : connection;

                if (connection == null)
                {
                    connection = new UserStudysetConnection(user, studyset, false);
                    studyset.Connections.Add(connection);
                }

                connection.IsStored = !connection.IsStored;
            });

            await _studysetDataAccess.UpdateAsync(studyset);
        }

        public async Task RateAsync(Studyset studyset, UserStudysetConnection connection, User user, float rating, string text)
        {
            await Task.Run(() =>
            {
                if (connection.Rating == null)
                {
                    connection.Rating = new Rating();
                }

                if (rating != connection.Rating.Value || text != connection.Rating.Description)
                {
                    connection.Rating.Value = rating;
                    connection.Rating.Description = text;

                }
            });

            await _studysetDataAccess.UpdateAsync(studyset);
        }

        public async Task<bool> AddContributorAsync(Studyset studyset, User user)
        {
            bool result = false;

            await Task.Run(() =>
            {
                if (studyset.Creator != user && studyset.Contributors.Contains(user) == false)
                {
                    studyset.Contributors.Add(user);
                    result = true;
                }
            });

            await _studysetDataAccess.UpdateAsync(studyset);
            return result;
        }

    }
}
