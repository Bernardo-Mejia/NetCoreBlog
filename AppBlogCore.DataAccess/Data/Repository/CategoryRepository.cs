using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppBlogCore.DataAccess.Data.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _db.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }

        public void Update(Category category)
        {
            Category initialCategory = _db.Categories.Find(category.Id);
            if (initialCategory != null)
            {
                initialCategory.Name = category.Name;
                initialCategory.Order = category.Order;

                _db.SaveChanges();
            }
        }
    }
}
