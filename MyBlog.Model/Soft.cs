using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
   public class Soft
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = true)]
        public string SoftName { get; set; }

        [SugarColumn(IsNullable = true)]
        public string USCC { get; set; }
    }
}
