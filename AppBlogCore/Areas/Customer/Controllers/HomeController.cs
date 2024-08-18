using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models;
using AppBlogCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppBlogCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                ArticlesList = _unitOfWork.Article.GetAll(),
                SlidersList = _unitOfWork.Slider.GetAll(s => s.Status == true),
            };

            ViewBag.IsHome = true;

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            Article article = _unitOfWork.Article.Get(id);
            if (article != null)
                return View(article);
            else
                return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
