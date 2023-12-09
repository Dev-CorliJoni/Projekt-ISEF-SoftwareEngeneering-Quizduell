using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quixduell.ServiceLayer.DataAccessLayer.Model.Answers;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Questions
{
    public class MultipleChoiceQuestion : BaseQuestion
    {
        public List<MultipleChoiceAnswer> Answers { get; set; }
    }
}
