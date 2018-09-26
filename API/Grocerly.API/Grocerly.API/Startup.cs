using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funq;
using Grocerly.Database;
using Grocerly.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Auth;
using ServiceStack.Caching;

namespace Grocerly.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<GrocerlyContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost());

            app.Run(context =>
            {
                context.Response.Redirect("/metadata");
                return Task.FromResult(0);
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GrocerlyContext>();
                context.Database.EnsureCreated();
            }

        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Grocerly", typeof(TagService).Assembly) { }

        public override void Configure(Container container)
        {
            this.Plugins.Add(new OpenApiFeature());
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
            new IAuthProvider[] {
                    new CredentialsAuthProvider(),
                    new JwtAuthProvider(AppSettings)
                    {
                        AuthKeyBase64 = "dGVzdA==",
                        RequireSecureConnection = false
                    }
            })
            {
                IncludeAssignRoleServices = false
            });
            Plugins.Add(new CorsFeature(
                allowedHeaders: "Authorization",
                allowedOrigins: "*"
            ));


            container.Register<ICacheClient>(new MemoryCacheClient());
            var userRep = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(userRep);

            var user = userRep.CreateUserAuth(new UserAuth
            {
                UserName = "test"
            }, "Test123");

            var admin = userRep.CreateUserAuth(new UserAuth
            {
                UserName = "Admin"
            }, "Test123");

            userRep.AssignRoles(user, new[] { "User" });

            userRep.AssignRoles(admin, new[] { "User" });
            userRep.AssignRoles(admin, new[] { "Admin" });
        }
    }

}
