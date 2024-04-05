using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AppBlogCore.Models.ViewModel
{
    public class ArticleVM
    {
        public Article Article { get; set; }

        //Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
