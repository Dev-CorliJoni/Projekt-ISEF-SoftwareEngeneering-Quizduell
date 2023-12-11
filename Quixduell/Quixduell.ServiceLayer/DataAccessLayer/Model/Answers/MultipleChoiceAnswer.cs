using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model.Answers
{
    public class MultipleChoiceAnswer : Answer
    {
        public bool IsTrue {  get; set; }

        public MultipleChoiceAnswer(Guid id, string text, bool isTrue) : base(id, text)
        {
            IsTrue = isTrue;
        }
    }
}
