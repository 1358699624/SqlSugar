using SqlSugar;
using System;
using System.ComponentModel;

namespace MyBlog.Model
{
    /// <summary>
    /// 学生信息模型
    /// </summary>
    public class UserInfo
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        public string UserName { get; set; }

        /// <summary>
        /// 学生性别
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        public string Sex { get; set; }

        /// <summary>
        /// 学生联系电话
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        public string Phone { get; set; }

        /// <summary>
        /// 学生描述
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        public string Description { get; set; }

        /// <summary>
        /// 学生爱好
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        public string Hobby { get; set; }
    }
}
