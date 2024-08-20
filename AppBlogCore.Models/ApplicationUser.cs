using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage = "Field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Country { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
