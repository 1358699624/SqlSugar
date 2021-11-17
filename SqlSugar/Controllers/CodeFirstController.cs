using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Utility;
using MySqlSugar.Utility;
using MySqlSugar.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CodeFirstController : ControllerBase
    {
        #region  依赖注入
        
        public readonly ICodeFirstService _icodeFirstService;

        public CodeFirstController(ICodeFirstService icodeFirstService)
        {
            _icodeFirstService = icodeFirstService;
        }
        #endregion

        /// <summary>
        /// 查询账号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetQuery")]
        public async Task<ActionResult<ApiResult>> GetQuery(string name)
        {
            try
            {
                var zhanghao = await _icodeFirstService.QueryAsync(c=> c.Name ==name);
                if (zhanghao.Count<=0)
                    return ApiResultHelper.Success("未查询到数据");
                return ApiResultHelper.Success(zhanghao);
            }
            catch (Exception ex)
            {

                return ApiResultHelper.Error(ex.Message);
            }

        }

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> GetCreate(string  name,string text)
        {
            try
            {

                var GETzh = await _icodeFirstService.QueryAsync(c => c.Name == name);
                if (GETzh.Count > 0) return ApiResultHelper.Error("创建账号失败,账号已存在");

                CodeFirstTable codeFirst = new CodeFirstTable
                {
                    Name = name,
                    Text = MD5Helper.MD5Encrypt32(text),
                    CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))
                };

                var zhanghao = await _icodeFirstService.CreateAsync(codeFirst);
                if (!zhanghao)
                    return ApiResultHelper.Error("创建账号失败");
                return ApiResultHelper.Success("创建账号成功");
            }
            catch (Exception ex)
            {

                return ApiResultHelper.Error(ex.Message);
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="text">密码</param>
        /// <returns></returns>
        [HttpPut("Editor")]
        public async Task<ActionResult<ApiResult>> Editor(string name,string text)
        {
            try
            {

                int id = Convert.ToInt32(this.User.FindFirst("Id").Value);//鉴权JWT使用
                var zhanghao = await _icodeFirstService.FindAsync(id);
                zhanghao.Text = MD5Helper.MD5Encrypt32(text);
                var b = await _icodeFirstService.UpdateAsync(zhanghao);
                if (!b)
                    return ApiResultHelper.Error("未查询到数据");
                return ApiResultHelper.Success(zhanghao);
                
            }
            catch (Exception ex)
            {

                return ApiResultHelper.Error(ex.Message);
            }

        }

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns> 
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(string name)
        {
            try
            {
                var zhanghao = await _icodeFirstService.GetAsync(c => c.Name == name);
                if (string.IsNullOrEmpty(zhanghao.Id.ToString()))
                    return ApiResultHelper.Error("未查询到数据");
                
                var b = await _icodeFirstService.DeleteAsync(zhanghao.Id);
                if (!b)
                    return ApiResultHelper.Error("未查询到数据");
                return ApiResultHelper.Success(zhanghao);

            }
            catch (Exception ex)
            {

                return ApiResultHelper.Error(ex.Message);
            }

        }


        /// <summary>
        /// DTO返回数据不带密码
        /// </summary>
        /// <param name="mapper">FromServices IMapper</param>
        /// <param name="id">int</param>
        /// <returns></returns>
        [AllowAnonymous]//匿名访问，不使用DTO
        [HttpGet("GetFirsAsync")]
        public async Task<ApiResult> GetFirsAsync([FromServices]IMapper mapper,int  id)//int id
        {
            try
            {
                /*
            //返回有MD5密码,有密码
            var codefirst = await _icodeFirstService.FindAsync(id);
            return ApiResultHelper.Success(codefirst);
            */
             
                
                //返回DTO 无密码
             
                
                var codefirst = await _icodeFirstService.FindAsync(id);//返回有密码
             
                
                var newcode = mapper.Map<CodeFirstDTO>(codefirst);//返回DTO
             
                
                return ApiResultHelper.Success(newcode);
            }
            catch (Exception ex)
            {

                return ApiResultHelper.Error(ex.Message);
            }



        }
    }
}
