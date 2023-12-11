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

        private MultipleChoiceAnswer() { }

        public MultipleChoiceAnswer(string text, bool isTrue) : base(text)
        {
            IsTrue = isTrue;
        }
    }
}
