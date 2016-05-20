using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAsp.Data.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public virtual HashSet<Article> Articles { get; set; }
    }


}
