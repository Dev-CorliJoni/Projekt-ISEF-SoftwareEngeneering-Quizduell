using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Questions
{
    internal class OpenQuestion : BaseQuestion
    {
        public Answer Answer {  get; set; }
    }
}
