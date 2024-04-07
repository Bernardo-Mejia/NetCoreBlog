using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnviroment;
        private readonly IUnitOfWork _unitOfWork;

        public ArticlesController(IWebHostEnvironment hostingEnviroment, IUnitOfWork unitOfWork)
        {
            _hostingEnviroment = hostingEnviroment;
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
                string rootPath = _hostingEnviroment.WebRootPath;
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

                    articleVM.Article.ImageURL = @"Images\Articles\" + fileName + extension;
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


        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Article.GetAll() });
        }

        #endregion
    }
}
