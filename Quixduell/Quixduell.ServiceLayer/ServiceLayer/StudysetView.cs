﻿using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task StarStudysetAsync(Studyset studyset, UserStudysetConnection connection)
        {
            await Task.Run(() =>
            {
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

    }
}
