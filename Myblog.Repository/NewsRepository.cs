
using MyBlog.IRepository;
using MyBlog.Model;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myblog.Repository
{
    public  class NewsRepository:BaseRepository <NewsTable>,INewsRepository
    {
    }
}
