using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myblog.Service;
using MyBlog.IService;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.Model;
using MySqlSugar.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace SqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoContext = userInfoService;

        }
        private readonly IUserInfoService  _userInfoContext;
        
        /// <summary>
        /// tdk
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async  Task<ActionResult<ApiResult>> GetUserInfo()
        {
            try
            {

                var data = await _userInfoContext.GetAsync();
                if (data == null)
                    return ApiResultHelper.Error("错误数据");

                //DbScoped.Sugar.Deleteable<UserInfo>().In(1).ExecuteCommand();
                return ApiResultHelper.Success(data);

                /*
                var data = await _userInfoContext.GetAsync();
                return Ok(data);
                */
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }
        /// <summary>
        /// 创建用户信息()
        /// </summary>
        /// <param name="title">用户名</param>
        /// <param name="content">爱好</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title,string  content)
        {
            try
            {

                UserInfo userInfo = new UserInfo
                {
                    UserName = title,
                    Phone = "1123321",
                    Sex = "女",
                    Hobby = content,
                    Description = "tdk"
                };

                var b = await _userInfoContext.CreateAsync(userInfo);
                if (!b) return ApiResultHelper.Error("创建错误");
                return ApiResultHelper.Success("创建成功");
            }
            catch (Exception ex)
            {
                return   ApiResultHelper.Error(ex.Message);

            }
        
        }

        /// <summary>
        /// 接受Json格式数据，并保存数据库，外部调用API
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateJson")]
        public async Task<ActionResult<ApiResult>> CreateJson(string message)
        {
            try
            {
                //
                var list2 = JObject.Parse(message);

                //将message转换为实体类格式
                UserInfo list = System.Text.Json.JsonSerializer.Deserialize<UserInfo>(message);
                UserInfo userInfo = new UserInfo
                {
                    UserName = list.UserName,
                    Phone = "1358699624",//list2["Phone"].ToString()
                    Sex = "女",
                    Hobby = list.Hobby,
                    Description = "tdk"
                };

                var b = await _userInfoContext.CreateAsync(userInfo);
                if (!b) return ApiResultHelper.Error("创建错误");
                return ApiResultHelper.Success("创建成功");
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);

            }

        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="id">id，后期传姓名即可修改信息</param>
        /// <param name="phone"></param>
        /// <param name="sex"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut("Editor")]
        public async Task<ActionResult<ApiResult>> Editor(int id,string phone,string  sex,string description)
        {
            try
            {
                var b = await _userInfoContext.FindAsync(id);
                if (b is null)
                {
                    return ApiResultHelper.Error("没有查询到数据");
                }
                b.Phone = phone;
                b.Sex = sex;
                b.Description = description;
                var bup = await _userInfoContext.UpdateAsync(b);
                if (!bup) return ApiResultHelper.Error("修改失败");
                return ApiResultHelper.Success("修改成功");
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);

            }

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">id，后期传姓名即可修改删除</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return ApiResultHelper.Error("id为空");
                }
                var b = await _userInfoContext.DeleteAsync(id);
                if (!b) return ApiResultHelper.Error("删除错误");
                return ApiResultHelper.Success("删除成功");
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);

            }

        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<ActionResult<ApiResult>> GetPage(int page, int  size) 
        {
            try
            {
                RefAsync<int> total = 0;
                var v = await _userInfoContext.QueryAsync(page, size, total);
                if (v.Count < 1)
                {
                    return ApiResultHelper.Success("未查询到数据");
                }
                return ApiResultHelper.Success(v, total);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="username">名称</param>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        [HttpGet("QueryAsync")]
        public async Task<ActionResult<ApiResult>> QueryAsyncResult(string username,string phone) 
        {
            try
            {
                var data = await _userInfoContext.QueryAsync(u => (u.UserName==username || u.UserName.Contains(username)) && u.Phone.Contains(phone));
                return ApiResultHelper.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }

        }
    }
}
