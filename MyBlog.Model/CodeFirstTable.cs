using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Model
{
   public class CodeFirstTable
    {
      
       [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
       public int Id { get; set; }
       public string Name { get; set; }
       [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
       public string Text { get; set; }
       [SugarColumn(IsNullable = true)]
       public DateTime CreateTime { get; set; }
      
    }
}
