using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Services
{
    public class Category : BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
