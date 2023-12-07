﻿using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Model.LernsetPagesModel
{
    /// <summary>
    /// Form Class for Updating existing Lernset
    /// </summary>
    public class EditLernset : ChangeLernset
    {
        public Guid ExistingLernsetID { get; set; }

        public EditLernset(Lernset lernset) : base(lernset.Creator) 
        {
            ExistingLernsetID = lernset.ID;
            Category = lernset.Category;
            Contributor = lernset.Contributors;
            Name = lernset.Name;
            Questions = lernset.Questions;

        }

    }
}
