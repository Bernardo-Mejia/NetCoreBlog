using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models;
using AppBlogCore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public SlidersController(IWebHostEnvironment hostingEnviroment, IUnitOfWork unitOfWork)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider model)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                IFormFileCollection files = HttpContext.Request.Form.Files;
                if (model.SliderId == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string uploads = Path.Combine(rootPath, @"Images\Sliders");
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    model.UrlImagen = @"\Images\Sliders\" + fileName + extension;

                    _unitOfWork.Slider.Add(model);
                    _unitOfWork.Save();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Slider slider = _unitOfWork.Slider.Get(id);
                return View(slider);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                IFormFileCollection files = HttpContext.Request.Form.Files;

                Models.Slider sliderFromDB = _unitOfWork.Slider.Get(slider.SliderId);

                if (files.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString(),
                        uploads = Path.Combine(rootPath, @"Images\Sliders"),
                        extension = Path.GetExtension(files[0].FileName);

                    string filePath = Path.Combine(rootPath, sliderFromDB.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    slider.UrlImagen = @"\Images\Sliders\" + fileName + extension;
                }
                else
                {
                    slider.UrlImagen = sliderFromDB.UrlImagen;
                }

                _unitOfWork.Slider.Update(slider);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }

            return View(slider);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new
            {
                data = _unitOfWork.Slider.GetAll()
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Models.Slider slider = _unitOfWork.Slider.Get(id);

            if (slider != null)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                string filePath = Path.Combine(rootPath, slider.UrlImagen.TrimStart('\\'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _unitOfWork.Slider.Remove(slider);
                _unitOfWork.Save();
                return Json(new { success = true, message = $"Slider {id} deleted succesfully" });
            }

            return Json(new { success = false, message = $"Error deleting Slider {id}" });
        }

        #endregion
    }
}
