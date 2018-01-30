using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Services.IdentityServer {
   public class Startup {
      public void ConfigureServices(IServiceCollection services) {

         services.AddMvc();

         services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
            .AddTestUsers(Config.GetUsers());

         services.AddAuthentication()
            .AddJwtBearer(options => {
               options.Authority = "http://localhost:5000";
               options.Audience = "routesApi";
               options.RequireHttpsMetadata = false;
            });
      }

      public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         }

         app.UseIdentityServer();

         app.UseStaticFiles();

         app.UseMvcWithDefaultRoute();

         app.Run(async (context) => {
            await context.Response.WriteAsync("Hello World!");
         });
      }
   }
}
