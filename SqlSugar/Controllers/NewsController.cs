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
    public class NewsController : ControllerBase
    {
        public readonly INewsService _inewservice;
        public NewsController(INewsService inewsService) 
        {
            _inewservice = inewsService;
        }

        [AllowAnonymous]
        [HttpGet("GetSouce")]
        public async Task<ApiResult> GetSouce() 
        {
            try
            {
                var souce = await _inewservice.GetAsync();

                return ApiResultHelper.Success(souce);

            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpGet("Page")]
        public async Task<ApiResult> GetPage(int page,int size) 
        {
            try
            {

                RefAsync<int> refAsync = 0;
                var pages = await _inewservice.QueryAsync(page, size, refAsync);

                return ApiResultHelper.Success(pages, refAsync);
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }
    }
}
