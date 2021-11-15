using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MySqlSugar.Utility.ApiResult;
using MySqlSugar.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTestController : ControllerBase
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize() 
        {
            return "No";
        }

        //使用JWT鉴权
        [Authorize]
        [HttpGet("Authorize")]
        public string Authorize()
        {
            return "YES";
        }


        //IMemory.Cache
        //缓存
        [TypeFilter(typeof(CustomResouceFilterAttribute))]
        [HttpGet("GetCache")]
        public IActionResult GetCache(string name) 
        {
            return new  JsonResult(new { 
                Id =1,
                Name = name,
                Sex = 18
            } );
        }

        [AllowAnonymous]
        [HttpGet("GetApiAsync")]
        public async Task<ActionResult<ApiResult>> GetApiAsync(string path)//调用API，Get请求，无参
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(path);
                string msg="";
                if (response.IsSuccessStatusCode)
                {
                    msg = await response.Content.ReadAsAsync<string>();
                }
                return ApiResultHelper.Success(msg);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpGet("GetApiAsync2")]
        public async Task<ActionResult<ApiResult>> GetApiAsync2(string path= "https://api.github.com/orgs/dotnet/repos")//调用API，Get请求，有参
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var  stream = await client.GetStreamAsync(path);
                var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
                return ApiResultHelper.Success(repositories);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }

        

        [AllowAnonymous]
        [HttpGet("PostApiAsync")]
        public async Task<ActionResult<ApiResult>> PostApiAsync(string path, string name)//调用API，Post请求，有参
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.BaseAddress = new Uri(path);
                //UserInfo userInfo = null;
                HttpResponseMessage response = await client.GetAsync(path);
                string msg = "";
                if (response.IsSuccessStatusCode)
                {
                    msg = await response.Content.ReadAsAsync<string>();
                }
                return ApiResultHelper.Success(msg);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }
    }
    public class Repository
    {
        public string name { get; set; }
    }
}
 