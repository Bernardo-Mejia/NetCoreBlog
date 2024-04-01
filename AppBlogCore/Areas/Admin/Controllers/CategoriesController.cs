﻿using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public CategoriesController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region API
        public IActionResult GetAll()
        {
            return Json(new { datat = _unitOfWork.Category.GetAll() });
        }
        #endregion
    }
}
