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

namespace SqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        
            var data = await _userInfoContext.GetAsync();

            if(data==null)
                return ApiResultHelper.Error("错误数据");

            //DbScoped.Sugar.Deleteable<UserInfo>().In(1).ExecuteCommand();
            return ApiResultHelper.Success(data);

            /*
            var data = await _userInfoContext.GetAsync();
            return Ok(data);
            */
        }

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


        [HttpGet("Page")]
        public async Task<ActionResult<ApiResult>> GetPage(int page, int  size) 
        {
            RefAsync<int> total = 0;
            var v = await _userInfoContext.QueryAsync(page,size,  total);
            return ApiResultHelper.Success(v,total);
        
        }
    }
}
