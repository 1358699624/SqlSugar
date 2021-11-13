using AutoMapper;
using MyBlog.Model;
using MySqlSugar.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Utility
{
   public class CodeFirstDTO:Profile
    {

        public CodeFirstDTO() 
        {
            CreateMap<CodeFirstTable, CodeFirstMapper>();
        }
    }
}
