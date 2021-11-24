using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Myblog.Service
{
    public class UserToNewsService : BaseService<UserAndNews>,IUserToNewsService
    {
        private readonly IUserToNewsRepository _iusertonewsRepository;

        public UserToNewsService(IUserToNewsRepository userToNewsService)
        {
            base._iBaseRepository = userToNewsService;
            _iusertonewsRepository = userToNewsService;
        }
    }
}
