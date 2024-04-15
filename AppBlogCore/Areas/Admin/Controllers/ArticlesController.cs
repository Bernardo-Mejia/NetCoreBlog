using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public ArticlesController(IWebHostEnvironment hostingEnviroment, IUnitOfWork unitOfWork)
        {
            _hostingEnvironment = hostingEnviroment;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM articuloVM = new()
            {
                Article = new Models.Article(),
                CategoryList = _unitOfWork.Category.GetListCategories()
            };
            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                IFormFileCollection files = HttpContext.Request.Form.Files;
                if (articleVM.Article.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string uploads = Path.Combine(rootPath, @"Images\Articles");
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    articleVM.Article.ImageURL = @"\Images\Articles\" + fileName + extension;
                    articleVM.Article.CreatedAt = DateTime.UtcNow;

                    articleVM.Article.Category = _unitOfWork.Category.Get(articleVM.Article.CategoryId);

                    _unitOfWork.Article.Add(articleVM.Article);
                    _unitOfWork.Save();

                    return RedirectToAction("Index");
                }
            }

            articleVM.CategoryList = _unitOfWork.Category.GetListCategories();
            return View(articleVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new Models.Article(),
                CategoryList = _unitOfWork.Category.GetListCategories(),
            };

            if (id != null)
            {
                articleVM.Article = _unitOfWork.Article.Get(id.GetValueOrDefault());
            }

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                IFormFileCollection files = HttpContext.Request.Form.Files;

                Models.Article articleFromDB = _unitOfWork.Article.Get(articleVM.Article.Id);

                if (files.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string uploads = Path.Combine(rootPath, @"Images\Articles");
                    string extension = Path.GetExtension(files[0].FileName);

                    string filePath = Path.Combine(rootPath, articleFromDB.ImageURL.TrimStart('\\'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    articleVM.Article.ImageURL = @"\Images\Articles\" + fileName + extension;
                }
                else
                {
                    articleVM.Article.ImageURL = articleFromDB.ImageURL;
                }

                _unitOfWork.Article.Update(articleVM.Article);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(articleVM);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Article.GetAll(includeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Models.Article article = _unitOfWork.Article.Get(id);

            if (article != null)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                string filePath = Path.Combine(rootPath, article.ImageURL.TrimStart('\\'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _unitOfWork.Article.Remove(article);
                _unitOfWork.Save();
                return Json(new { success = true, message = $"Article {id} deleted succesfully" });
            }

            return Json(new { success = false, message = $"Error deleting article {id}" });
        }

        #endregion
    }
}
