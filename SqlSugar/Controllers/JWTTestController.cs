using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlSugar.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
