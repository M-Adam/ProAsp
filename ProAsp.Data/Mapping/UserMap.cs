using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data.Models;

namespace ProAsp.Data.Mapping
{
    public partial class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("Users");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).IsRequired();
        }
    }
}
