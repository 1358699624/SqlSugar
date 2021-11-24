
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;

namespace MyBlog.IService
{
     public  interface IUserToNewsService : IBaseService<UserAndNews>
    {
    }
}
