using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public abstract class IdModel
    {
        [Key]
        public Guid Id { get; set; }

        protected IdModel() { }

        protected IdModel(Guid id)
        {
            this.Id = id;
        }
    }
}
