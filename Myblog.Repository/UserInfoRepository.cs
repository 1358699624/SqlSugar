using MyBlog.IRepository;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;

namespace Myblog.Repository
{
  public   class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
    }
}
