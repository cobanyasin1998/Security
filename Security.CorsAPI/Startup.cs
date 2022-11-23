using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.CorsAPI
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
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {


            //        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //    });
            //});

            services.AddCors(opt =>
            {

                opt.AddPolicy("AllowSites", builder =>
                {
                    builder.WithOrigins("https://localhost:44322").AllowAnyHeader().WithMethods("POST");

                });

            });

            services.AddCors(opt =>
            {

                opt.AddPolicy("AllowSites2", builder =>
                {
                    builder.WithOrigins("https://localhost:44322").AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains();

                });

            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSites2");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
