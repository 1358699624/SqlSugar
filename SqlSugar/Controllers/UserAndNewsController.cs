using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MySqlSugar.Utility.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAndNewsController : ControllerBase
    {


        public readonly IUserToNewsService _iuserToNewsService;
        /// <summary>
        /// 构造函数 依赖注入
        /// </summary>
        /// <param name="userToNewsService"></param>
        public UserAndNewsController(IUserToNewsService userToNewsService) 
        {
            _iuserToNewsService = userToNewsService;
        }
        /// <summary>
        /// 多表查询
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]//取消鉴权
        [HttpGet("GetUserToNews")]
        public async Task<ApiResult> GetUserToNews(int page, int size)
        {
            try
            {
                RefAsync<int> refAsync = 0;
                var pages = await _iuserToNewsService.QueryAsync(page, size, refAsync);

                return ApiResultHelper.Success(pages, refAsync);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }
    }
}
