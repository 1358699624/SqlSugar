using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize]
        [HttpGet("Authorize")]
        public string Authorize()
        {
            return "YES";
        }
    }
}
