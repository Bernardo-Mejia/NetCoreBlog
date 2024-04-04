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
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void IArticleRepository.Update(Article article)
        {
            Article initialArticle = _db.Articles.Find(article.Id);
            if (initialArticle != null)
            {
                initialArticle.Name = article.Name;
                initialArticle.Description = article.Description;
                initialArticle.ImageURL = article.ImageURL;
                initialArticle.CategoryId = article.CategoryId;

                //_db.SaveChanges();
            }
        }
    }
}
