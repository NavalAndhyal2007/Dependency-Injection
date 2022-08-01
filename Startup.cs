using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DIWebApiTutorial
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

            services.AddControllers();
            //services.AddSingleton<IStudentRepository, StudentRepository>();
            //services.AddSingleton(typeof(IStudentRepository), typeof(StudentRepository));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

            services.AddMemoryCache();
            //services.AddTransient<IStudentRepository, StudentRepository>();
            //services.AddTransient(typeof(IStudentRepository), typeof(StudentRepository));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                //options.ApiVersionReader = new HeaderApiVersionReader("EMP-API-VERSION");
                options.ApiVersionReader = new QueryStringApiVersionReader("emp-api-version");
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44376")
                    //builder.WithOrigins("https://localhost:44377/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    //    .WithHeaders("emp-header-version");
                    //sbuilder.AllowAnyOrigin();
                });
            });

            services.AddDbContextPool<EmployeeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBConnectionString")));
            services.AddScoped<IEmployeeService, EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            //app.UseHttpsRedirection();
            app.UseCors();

            //app.Use(c)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "api/{controller}/{action}/{id}",
                //    defaults: new
                //    {
                //        id = RouteParameter.Optional
                //    }
                //);
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoint
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=EmployeeApi}/{action=GetEmployees}/{id?}");
            //});
        }
    }
}
