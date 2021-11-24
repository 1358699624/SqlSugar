using MyBlog.IRepostitory;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface ISoftRepository : IBaseRepository<Soft>
    {
        /// <summary>
        /// 使用存储过程
        /// </summary>
        /// <returns></returns>
        Task<DataTable> GetSoftAsync(string  sql,string dic);
    }
}
