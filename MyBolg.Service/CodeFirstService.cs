using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;
using MyBlog.IRepository;

namespace Myblog.Service
{
    public class CodeFirstService:BaseService<CodeFirstTable>,ICodeFirstService
    {
        private readonly ICodeFirstTableRepository _iCodeFirstRepository;

        public CodeFirstService(ICodeFirstTableRepository iCodeFirstRepository)
        {
            base._iBaseRepository = iCodeFirstRepository;
            _iCodeFirstRepository = iCodeFirstRepository;
        }

    }
}
