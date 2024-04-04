using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [Display(Name = "Article name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public required Category Category { get; set; }

    }
}
