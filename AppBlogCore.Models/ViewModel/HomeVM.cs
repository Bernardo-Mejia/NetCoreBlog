using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Slider> SlidersList { get; set; }
        public IEnumerable<Article> ArticlesList { get; set; }
    }
}
