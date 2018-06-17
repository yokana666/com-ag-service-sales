using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Spinning;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Spinning;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Weaving;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace Com.Danliris.Service.Sales.WebApi
{
    public class Startup
    {
        /* Hard Code */
        private string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private string SALES_POLICY = "SalesPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Register

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<WeavingSalesContractFacade>()
                .AddTransient<SpinningSalesContractFacade>()
                .AddTransient<FinishingPrintingSalesContractFacade>();
        }

        private void RegisterLogic(IServiceCollection services)
        {
            services
                .AddTransient<WeavingSalesContractLogic>()
                .AddTransient<SpinningSalesContractLogic>()
                .AddTransient<FinishingPrintingSalesContractLogic>()
                .AddTransient<FinishingPrintingSalesContractDetailLogic>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IdentityService>()
                .AddScoped<ValidateService>();
        }

        #endregion Register
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            /* Register */
            services.AddDbContext<SalesDbContext>(options => options.UseSqlServer(connectionString));
            RegisterFacades(services);
            RegisterLogic(services);
            RegisterServices(services);
            services.AddAutoMapper();

            /* Versioning */
            services.AddApiVersioning(options => { options.DefaultApiVersion = new ApiVersion(1, 0); });

            /* Authentication */
            string Secret = Configuration.GetValue<string>("Secret") ?? Configuration["Secret"];
            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = Key
                    };
                });

            /* CORS */
            services.AddCors(options => options.AddPolicy(SALES_POLICY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));

            /* API */
            services
               .AddMvcCore()
               .AddAuthorization()
               .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
               .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Update Database */
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                SalesDbContext context = serviceScope.ServiceProvider.GetService<SalesDbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseCors(SALES_POLICY);
            app.UseMvc();
        }
    }
}
