using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProAsp.Viewmodels
{
    public class ArticleViewmodel
    {
        [Editable(false)]
        public Guid Id { get;set; }

        [Required]
        public string Title { get; set; }


        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        [Editable(false)]
        public string CreatorName { get; set; }
    }
}