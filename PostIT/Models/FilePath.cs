using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostIT.Models
{
    public class FilePath
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}