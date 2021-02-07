using Application.CQRS;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
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
            var Identity = services.AddIdentity<AppUser,Role>(opt=>
                {
                    var PasswordManager = opt.Password;
                    PasswordManager.RequireDigit = false;
                    PasswordManager.RequireLowercase = false;
                    PasswordManager.RequireNonAlphanumeric = false;
                    PasswordManager.RequireUppercase = false;
                }
                );
            var IdentityBuilder = new IdentityBuilder(Identity.UserType, typeof(Role), Identity.Services);
            IdentityBuilder.AddEntityFrameworkStores<DataContext>();
            IdentityBuilder.AddUserManager<UserManager<AppUser>>();
            IdentityBuilder.AddSignInManager<SignInManager<AppUser>>();
            IdentityBuilder.AddRoleValidator<RoleValidator<Role>>();
            IdentityBuilder.AddRoleManager<RoleManager<Role>>();

            services.AddDbContext<DataContext>(opt => {
                opt.UseNpgsql(Configuration.GetConnectionString("Default"));
            });
            services.AddMediatR(typeof(ReadUsers.handler).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
