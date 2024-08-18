using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Article = new ArticleRepository(_db);
            Slider = new SliderRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IArticleRepository Article { get; private set; }

        public ISliderRepository Slider { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
