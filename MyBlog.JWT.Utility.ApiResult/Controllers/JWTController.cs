using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.JWT.Utility.ApiResult.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        public readonly ICodeFirstService _icodeFirstService;
        public JWTController(ICodeFirstService codeFirstService) 
        {
            _icodeFirstService = codeFirstService;
        }


        [HttpPost("Login")]
        public async Task<ApiResult> Login(string name) 
        {
            try
            {
                //数据校验

                var jtw = await _icodeFirstService.GetAsync(c => c.Name == name);

                if (jtw != null)
                {
                    //登陆成功
                    var claims = new Claim[]
                        {
                new Claim(ClaimTypes.Name, jtw.Name),
                new Claim("Id", jtw.Id.ToString()),//此Id非常重要，否则后面如果用到id还要重新查询用户表
                new Claim("Name", jtw.Name)
                            //不能放敏感信息 
                        };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));//密钥16位
                    //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                    var token = new JwtSecurityToken(
                        issuer: "http://localhost:6060",
                        audience: "http://localhost:5000",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddHours(1),//1小时过期
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return ApiResultHelper.Success(jwtToken);
                }
                else
                {
                    return ApiResultHelper.Success("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return ApiResultHelper.Error(ex.Message);
            }
        }
    }
}
