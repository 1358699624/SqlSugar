using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;

namespace Myblog.Service
{
    public class NewsService : BaseService<NewsTable>, INewsService
    {
        private readonly INewsRepository _iNewsRepository;

        public NewsService(INewsRepository iNewsRepository)
        {
            base._iBaseRepository = iNewsRepository;
            _iNewsRepository = iNewsRepository;
        }
    }
}
