using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DeliveryService.Services.WebApi {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services) {

         services.AddMvcCore()
            .AddAuthorization(options => {
               options.AddPolicy("AdminOnly", policyAdmin => {
                  policyAdmin.RequireClaim("role", "Admin");
               });
            })
            .AddJsonFormatters();

         services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(options => {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;
                options.RoleClaimType = ClaimTypes.Role;
                options.ApiName = "routesApi";
             });

         services.AddCors(options => {
            options.AddPolicy("default", policy => {
               policy.WithOrigins("http://localhost:5000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            });
         });

         DeliveryService.IoC.IoCConfiguration.Configure(Configuration, services);
         services.AddMvc();

         services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new Info { Title = "Delivery Service API", Version = "v1" });
            // Handle OAuth
            options.AddSecurityDefinition("oauth2", new OAuth2Scheme {
               Type = "oauth2",
               Flow = "implicit",
               AuthorizationUrl = "http://localhost:5000/connect/authorize",
               TokenUrl = "http://localhost:5000/connect/token",
               Scopes = new Dictionary<string, string>()
               {
                  { "routesApi", "Delivery Service API" }
               }
            });
            options.OperationFilter<AuthorizeCheckOperationFilter>();
         });
      }

      public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         }

         app.UseSwagger();
         app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery Service API V1");
            options.ConfigureOAuth2("swaggerui", "", "", "Swagger UI");
         });

         app.UseCors("default");
         app.UseAuthentication();

         app.UseMvc();
         DeliveryService.IoC.IoCConfiguration.ConfigureDb(app.ApplicationServices);
      }
   }
}
