using AppBlogCore.Data;
using AppBlogCore.DataAccess.Data.Repository.IRepository;
using AppBlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.DataAccess.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Slider>> GetListSliders()
        {
            return await _db.Sliders.ToListAsync();
        }

        public IEnumerable<Slider> GetListSlidersActive()
        {
            return _db.Sliders.Where(s => s.Status == true).ToList();
        }

        public void Update(Slider slider)
        {
            if (slider != null)
            {
                Slider initialSlider = _db.Sliders.Find(slider.SliderId)!;
                if (initialSlider != null)
                {
                    initialSlider.Name = !String.IsNullOrEmpty(slider.Name) ? slider.Name : initialSlider.Name;
                    initialSlider.UrlImagen = !String.IsNullOrEmpty(slider.UrlImagen) ? slider.UrlImagen : initialSlider.UrlImagen;
                    initialSlider.Status = slider.Status;

                    _db.SaveChanges();
                }
            }
        }
    }
}
