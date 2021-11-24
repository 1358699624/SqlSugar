using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlSugar.Utility.ApiResult
{
    /// <summary>
    /// 返回类
    /// </summary>
    public static class ApiResultHelper
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ApiResult Success(dynamic data) 
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                msg = "操作成功",
                Total = 0
            };
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="total">数据总量</param>
        /// <returns></returns>
        public static ApiResult Success(dynamic data,RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                msg = "操作成功",
                Total = total
            };
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg">ex</param>
        /// <returns></returns>
        public static ApiResult Error(string  msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                msg = msg,
                Total = 0
            };
        }


    }
}
