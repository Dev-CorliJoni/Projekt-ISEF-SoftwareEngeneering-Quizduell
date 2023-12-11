using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixduell.ServiceLayer.DataAccessLayer.Model
{
    public class IdModel
    {
        [Key]
        public Guid Id { get; set; }

        // Isd Constructor needed
        //public BaseModel(Guid id) 
        //{ 
        //    this.Id = id;
        //}
    }
}
