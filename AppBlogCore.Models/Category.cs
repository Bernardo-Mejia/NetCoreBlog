using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a name for the category")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Display order")]
        public Nullable<int> Order { get; set; }
    }
}
