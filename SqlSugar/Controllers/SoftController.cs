using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MySqlSugar.Utility.ApiResult;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MySqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SoftController : ControllerBase
    {
        public readonly ISoftRepository _isoftService;

        public SoftController(ISoftRepository isoftService)
        {
            _isoftService = isoftService;
        }
        /// <summary>
        /// 根据企业名称查询企业信息
        /// </summary>
        /// <param name="softname">企业名称</param>
        /// <returns>List Soft实体类</returns>
        [HttpGet("GetDataTableAnysc")]
        [AllowAnonymous]
        public async Task<ApiResult> GetDataTableAnysc(string softname)
        {
            try
            {

                DataTable v = await _isoftService.GetSoftAsync("UP_Soft", softname);

               List<Soft> strBody = DataTableToModel<Soft>(v);


                return ApiResultHelper.Success(strBody);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }

        /// <summary>
        /// DataTable转为List泛型
        /// </summary>
        /// <typeparam name="T">泛型列表</typeparam>
        /// <param name="dt">数据源</param>
        /// <returns>model数据</returns>
        public static List<T> DataTableToModel<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = DataRowToModel<T>(row);
                data.Add(item);
            }
            return data;
        }
        /// <summary>
        /// 将DataRow转换成实体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>实体类</returns>
        public static T DataRowToModel<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (dr[column.ColumnName] == DBNull.Value)
                        {
                            if (column.DataType.Name == "DateTime")
                                pro.SetValue(obj, new DateTime(), null);
                            else
                                pro.SetValue(obj, " ", null);
                            break;
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                            break;
                        }
                    }
                }
            }
            return obj;
        }
    }
}