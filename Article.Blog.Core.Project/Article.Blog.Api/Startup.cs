﻿using Article.Blog.Common.Config;
using Article.Blog.Common.Mapper.ArticleMap;
using Article.Blog.Common.Mapper.CommentMap;
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
            //Read appsetting.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //NLog
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Read appsetting.json
            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //In-Memory Cache
            services.AddMemoryCache();

            //ConnectionString
            services.AddDbContext<ArticleBlogContext>(options => options.UseSqlServer(Configuration["dbConnection"]));

            //Swagger API UI
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Core API", Description = "Article Blog API" });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"articleBlog.API.xml";

                x.IncludeXmlComments(xmlPath);
            }
            );

            //NLog
            services.AddSingleton<ILog, LogNLog>();

            //Dependency
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            //Mapping
            services.AddAutoMapper(x => x.AddProfile(new UserMapping()));
            services.AddAutoMapper(x => x.AddProfile(new ArticleMapping()));
            services.AddAutoMapper(x => x.AddProfile(new CommentMapping()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Swagger API UI
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
