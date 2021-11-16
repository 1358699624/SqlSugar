using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MySqlSugar.Utility;
using MySqlSugar.Utility.ApiResult;
using MySqlSugar.Utility.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        [HttpGet("GetApiReadAsAsync")]
        public async Task<ActionResult<ApiResult>> GetApiReadAsAsync(string path)//调用API，Get请求，无参
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
        [HttpGet("GetStreamAsync")]
        public async Task<ActionResult<ApiResult>> GetStreamAsync(string path= "https://api.github.com/orgs/dotnet/repos")//调用API，Get请求,无参
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
                var repositories = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Repository>>(stream);
                return ApiResultHelper.Success(repositories);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }



        /// <summary>
        /// application/json
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetApiAsyncToJson")]
        public async Task<JsonResult> GetApiAsync3(string path, string name)//调用API，Post请求，有参
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = path + $"?name={name}";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(url);
                var msg = "";
                if (response.IsSuccessStatusCode)
                {
                    //msg = await response.Content.ReadAsAsync<string>();
                    msg = await response.Content.ReadAsStringAsync(); 
                }
                return  new  JsonResult(msg, new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
            catch (Exception ex)
            {
                return   new JsonResult(ApiResultHelper.Success(ex.Message), new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
        }


        /// <summary>
        /// application/json
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("PostApiAsync")]
        public async Task<ActionResult<ApiResult>> PostApiAsync(string path, string message)//调用API，Post请求，有参
        {
            try
            {

                var gizmo = new UserInfo() { UserName = "Gizmo", Hobby = "Widget" };

                var content = new StringContent(message, Encoding.UTF8, "application/json");//请求参数   请求消息内容
                //httpClient.DefaultRequestHeaders.Add("Method", "Post");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                path += $"?message={message}";

                //MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                //HttpContent content = new ObjectContent<UserInfo>(gizmo, jsonFormatter);

                var response = await client.PostAsJsonAsync(path, content);
                    return new JsonResult(response.Headers.Location, new JsonSerializerOptions
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message, new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
        }
    }
    
    public class Repository
    {
        public string name { get; set; }
    }
}
 