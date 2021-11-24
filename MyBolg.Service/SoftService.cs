using Myblog.Service;
using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myblog.Service
{
    public class SoftService : BaseService<Soft>
    {
        private readonly ISoftRepository _isoftRepository;

        public SoftService(ISoftRepository   iUserInfoRepository)
        {
            base._iBaseRepository = iUserInfoRepository;
            _isoftRepository = iUserInfoRepository;
        }
    }
}
