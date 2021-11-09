using MyBlog.IRepostitory;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
     public interface INewsRepository : IBaseRepository<NewsTable>
    {
    }
}
