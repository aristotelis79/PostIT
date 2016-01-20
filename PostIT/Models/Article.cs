using System;
using System.ComponentModel.DataAnnotations;

namespace PostIT.Models
{
    public class Article
    {
        public int ID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("Content")]
        [MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long")]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [UIHint("Content")]
        public string ShortDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime? Modified { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Published { get; set; }

        public int CategoryID { get; set; }

        public virtual Tag Category { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}