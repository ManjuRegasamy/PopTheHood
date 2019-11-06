using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using PopTheHood.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace PopTheHood
{
    public class Startup
    {

       // public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        //public Startup(IHostingEnvironment env)
        //{

        //}
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //For JWTAuthentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //google auth
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "293027629796-c2b1ps0nhnmm4h2ugooruq21hdc4msng.apps.googleusercontent.com";
                    options.ClientSecret = "xHM9gOigdt0CfXLDNgtbTnKY";
                });




            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });

            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PopTheHood", Description = "Swagger Core API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                //  c.OperationFilter<SecurityRequirementsOperationFilter>();
                
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
                });

                //var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"PopTheHood.xml";
                //c.IncludeXmlComments(xmlPath);

                //var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{_webApiAssemblyName}.xml");
                //options.IncludeXmlComments(filePath);
                //options.DescribeAllEnumsAsStrings();
                ////this is the step where we add the operation filter
                //options.OperationFilter<FileUploadOperation>();


            });


            //google auth
            //services.AddAuthentication()
            //.AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection =
            //        Configuration.GetSection("293027629796-c2b1ps0nhnmm4h2ugooruq21hdc4msng.apps.googleusercontent.com");
            //    IConfigurationSection googleAuthNSectionSecret =
            //        Configuration.GetSection("xHM9gOigdt0CfXLDNgtbTnKY");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSectionSecret["ClientSecret"];
            //});

            //.AddFacebook(facebookOptions => 
            //{
            //    IConfigurationSection googleAuthNSection =
            //        Configuration.GetSection("293027629796-c2b1ps0nhnmm4h2ugooruq21hdc4msng.apps.googleusercontent.com");
            //    IConfigurationSection googleAuthNSectionSecret =
            //        Configuration.GetSection("xHM9gOigdt0CfXLDNgtbTnKY");

            //    facebookOptions.ClientId = googleAuthNSection["ClientId"];
            //    facebookOptions.ClientSecret = googleAuthNSectionSecret["ClientSecret"];
            //});

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
                app.UseHsts();
            }

            //For JWTAuthentication
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PopTheHood");
            });
            app.UseAuthentication();


            //Google Authentication
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    //ClientId = "",
            //    //ClientSecret = ""
            //});

        }
    }
}
