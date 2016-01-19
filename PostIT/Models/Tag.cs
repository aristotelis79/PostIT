using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostIT.Models
{
    public class Tag
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}