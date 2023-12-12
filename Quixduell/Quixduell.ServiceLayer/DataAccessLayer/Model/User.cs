﻿using Microsoft.AspNetCore.Identity;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class User : IdentityUser
    {
        public List<UserStudysetConnection> StudysetConnections { get; set; }


        public User()
        {
            StudysetConnections = new List<UserStudysetConnection>();
        }
    }
}