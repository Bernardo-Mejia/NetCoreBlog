using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlogCore.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // REPOS
        ICategoryRepository Category { get; }

        void Save();
    }
}
