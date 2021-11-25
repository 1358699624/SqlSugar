using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Myblog.Repository;
using Myblog.Service;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Utility;
using MySqlSugar.Utility;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;


namespace SqlSugar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppConfig.Appsetting = Configuration["MySqlConnection"];

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Myblog.WebApi", Version = "v2" });

                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                c.IncludeXmlComments(xmlPath, true);


                #region Swagger使用鉴权组件
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
                #endregion
            });
            services.AddHttpClient();

            #region 跨域

            // 配置跨域处理，允许所有来源

            services.AddCors(options => {

                // this defines a CORS policy called "default"

                options.AddPolicy("default", policy => {

                    policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

                });

            });
            #endregion

            #region SqlSugar


            services.AddSqlSugar(new IocConfig()
            {
                //ConfigId="db01"  多租户用到
                ConnectionString = AppConfig.Appsetting,
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });
            #endregion
            services.AddCustomIOC();


            //鉴权
            services.AddCustomJWT();

            //使用AutoMapper   DTO
            services.AddAutoMapper(typeof(CodeFirstMapper));


            //使用缓存
            services.AddMemoryCache();

            #region MyRegion注释

            /* 
            //无法使用
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppConfig.Appsetting,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });*/

            /*
            //控制器返回json中文不乱码
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });*/
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerAPI v1"));

            }

            app.UseCors("default");
            app.UseRouting();
            //添加到管道中   JWT 鉴权
            app.UseAuthentication();
            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<ICodeFirstTableRepository, CodeFirstTableRepository>();
            services.AddScoped<ICodeFirstService, CodeFirstService>();

            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            
            services.AddScoped<IUserInfoService, UserInfoService>();


            services.AddScoped<IUserToNewsRepository, UserToNewsRepository>();
            services.AddScoped<IUserToNewsService, UserToNewsService>();


            services.AddScoped<ISoftRepository, SoftRepository>();


            return services;
        }
        #region  鉴权

        
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                          ValidateIssuer = true,
                          ValidIssuer = "http://localhost:6060",
                          ValidateAudience = true,
                          ValidAudience = "http://localhost:5000",
                          ValidateLifetime = true,
                          ClockSkew = TimeSpan.FromMinutes(60)
                      };
                  });
            return services;
        }
        #endregion
    }

  
}
