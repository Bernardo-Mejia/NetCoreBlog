using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ArticlesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
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


        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Article.GetAll() });
        }

        #endregion
    }
}
