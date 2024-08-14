using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.Models
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Slider Name")]
        public string Name { get; set; }

        public Nullable<bool> Status { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UrlImagen { get; set; }
    }
}
