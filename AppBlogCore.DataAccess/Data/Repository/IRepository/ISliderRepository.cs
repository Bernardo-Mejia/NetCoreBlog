﻿using AppBlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.DataAccess.Data.Repository.IRepository
{
    public interface ISliderRepository: IRepository<Slider>
    {
        Task<IEnumerable<Slider>> GetListSliders();
        IEnumerable<Slider> GetListSlidersActive();

        void Update(Slider slider);
    }
}
