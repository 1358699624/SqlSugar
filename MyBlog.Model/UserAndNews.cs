using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
   public class UserAndNews
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Sex { get; set; }


        public string Phone { get; set; }

        public string Description { get; set; }


        public string Hobby { get; set; }

        public string Types { get; set; }//类型

        public string Text { get; set; }


        public DateTime CreateTime { get; set; }

        public int codeFirstId { get; set; }
        
        public int userInfoId { get; set; }
        
    }
}
