using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data.Models;

namespace ProAsp.Data.Mapping
{
    public partial class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.ToTable("Articles");
            this.HasKey(x => x.Id);
            this.Property(x => x.Title).IsRequired();
        }
    }
}
