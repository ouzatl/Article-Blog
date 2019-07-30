using Article.Blog.Common.Config;
using Article.Blog.Common.Mapper.UserMap;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Data;
using Article.Blog.Data.Models;
using Article.Blog.Repository.Repositories.ArticleRepository;
using Article.Blog.Repository.Repositories.CommentRepository;
using Article.Blog.Repository.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Article.Blog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMemoryCache();

            services.AddDbContext<ArticleBlogContext>(options => options.UseSqlServer(Configuration["dbConnection"]));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Core API", Description = "Article Blog API" });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"articleBlog.API.xml";

                x.IncludeXmlComments(xmlPath);
            }
            );

            services.AddSingleton<ILog, LogNLog>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddAutoMapper(x => x.AddProfile(new UserMapping()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Article Blog API");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
