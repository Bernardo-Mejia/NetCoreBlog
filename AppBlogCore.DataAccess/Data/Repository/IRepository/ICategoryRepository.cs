using AppBlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppBlogCore.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        IEnumerable<SelectListItem> GetListCategories();

        void Update(Category category);
    }
}
