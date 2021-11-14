using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlSugar.Utility.Filter
{
    //:特性,资源过滤器/
    //asp .net core 过滤器   方法过滤器/异常/资源/授权
    public class CustomResouceFilterAttribute : Attribute, IResourceFilter
    {
        //依赖注入
        private readonly IMemoryCache _mamoryCaache;
        //构造函数
        public CustomResouceFilterAttribute(IMemoryCache mamoryCaache) 
        {
            _mamoryCaache = mamoryCaache;
        }

        
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //api/controller/getcache
            string path = context.HttpContext.Request.Path;

            //参数?name=name
            string canshu = context.HttpContext.Request.QueryString.Value;
            //api/controller/getcache?name=name
            string key = path + canshu;
            //设置缓存key  和  值
            _mamoryCaache.Set(key,context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //api/controller/getcache
            string path = context.HttpContext.Request.Path;

            //参数?name=name
            string canshu = context.HttpContext.Request.QueryString.Value;
            //api/controller/getcache?name=name
            string key = path + canshu;


            //查询缓存key  和  值
            if (_mamoryCaache.TryGetValue(key,out object value))
            {
                context.Result = value as IActionResult;
            }
        }
    }
}
