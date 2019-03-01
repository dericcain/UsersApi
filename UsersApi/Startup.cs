
using System;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersApi.Data;

namespace UsersApi
{
    public class Startup
    {
        private readonly string _host;
        private readonly string _database;
        private readonly string _user;
        private readonly string _password;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // We don't want to throw here because our environment variables may be loaded without an .env file
            try
            {
                Env.Load();
            }
            catch (Exception e)
            {
                
            }
            
            _host = Env.GetString("DB_HOST");
            _database = Env.GetString("DB_NAME");
            _user = Env.GetString("DB_USER");
            _password = Env.GetString("DB_PASSWORD");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<UsersDbContext>(options =>
                options.UseMySql($"Server={_host};Database={_database};User={_user};Password={_password}"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
