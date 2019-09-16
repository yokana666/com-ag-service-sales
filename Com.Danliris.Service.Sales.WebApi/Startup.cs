using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.Helpers;
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
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Weaving;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Spinning;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.ROGarmentInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ROGarment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.Garment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.GarmentSewingBlockingPlanInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MaxWHConfirmInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;

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
                .AddTransient<IWeavingSalesContract, WeavingSalesContractFacade>()
                .AddTransient<ISpinningSalesContract, SpinningSalesContractFacade>()
                .AddTransient<SpinningSalesContractReportFacade>()
                .AddTransient<FinishingPrintingSalesContractReportFacade>()
                .AddTransient<IGarmentBookingOrderMonitoringInterface, GarmentBookingOrderMonitoringFacade>()
                .AddTransient<IFinishingPrintingSalesContract, FinishingPrintingSalesContractFacade>()
                .AddTransient<IGarmentSalesContract, GarmentSalesContractFacade>()
                .AddTransient<IProductionOrder, ProductionOrderFacade>()
                .AddTransient<WeavingSalesContractReportFacade>()
				.AddTransient<ICostCalculationGarment,CostCalculationGarmentFacade>()
                .AddTransient<ICostCalculationGarmentByUnitReport, CostCalculationGarmentByUnitReportFacade>()
                .AddTransient<IROGarment, ROGarmentFacade>()
                .AddTransient<IArticleColor, ArticleColorFacade>()
                .AddTransient<IRate, RateFacade>()
                .AddTransient<IEfficiency, EfficiencyFacade>()
                .AddTransient<IAzureImageFacade, AzureImageFacade>()
                .AddTransient<IGarment_BudgetValidationPPIC, Garment_BudgetValidationPPICFacade>()
                .AddTransient<IGarmentBookingOrder, GarmentBookingOrderFacade>()
                .AddTransient<IWeeklyPlanFacade, WeeklyPlanFacade>()
                .AddTransient<ICanceledGarmentBookingOrderReportFacade, CanceledGarmentBookingOrderReportFacade>()
                .AddTransient<IMonitoringRemainingEHFacade, MonitoringRemainingEHFacade>()
                .AddTransient<IGarmentSewingBlockingPlan, GarmentSewingBlockingPlanFacade>()
                .AddTransient<IAcceptedOrderMonitoringFacade, AcceptedOrderMonitoringFacade>()
                .AddTransient<IExpiredGarmentBookingOrder, ExpiredGarmentBookingOrderFacade>()
                .AddTransient<ISewingBlockingPlanReportFacade, SewingBlockingPlanReportFacade>()
                .AddTransient<IOverScheduleMonitoringFacade, OverScheduleMonitoringFacade>()
                .AddTransient<IWeeklyWorkingScheduleMonitoringFacade, WeeklyWorkingScheduleMonitoringFacade>()
                .AddTransient<IMaxWHConfirmFacade, MaxWHConfirmFacade>()
                .AddTransient<IGarmentPreSalesContract, GarmentPreSalesContractFacade>();
        }

        private void RegisterLogic(IServiceCollection services)
        {

			services
				.AddTransient<WeavingSalesContractLogic>()
				.AddTransient<SpinningSalesContractLogic>()
				.AddTransient<FinishingPrintingSalesContractLogic>()
				.AddTransient<FinishingPrintingSalesContractDetailLogic>()
				.AddTransient<ProductionOrder_DetailLogic>()
				.AddTransient<ProductionOrder_LampStandardLogic>()
				.AddTransient<ProductionOrder_RunWidthLogic>()
				.AddTransient<ProductionOrderLogic>()
				.AddTransient<CostCalculationGarmentLogic>()
				.AddTransient<CostCalculationGarmentMaterialLogic>()
                .AddTransient<CostCalculationByUnitReportLogic>()
                .AddTransient<GarmentSalesContractLogic>()
                .AddTransient<GarmentSalesContractItemLogic>()
                .AddTransient<ArticleColorLogic>()
                .AddTransient<ROGarmentLogic>()
                .AddTransient<ROGarmentSizeBreakdownLogic>()
                .AddTransient<ROGarmentSizeBreakdownDetailLogic>()
                .AddTransient<RateLogic>()
                .AddTransient<EfficiencyLogic>()
                .AddTransient<Garment_BudgetValidationPPICLogic>()
                .AddTransient<GarmentBookingOrderLogic>()
                .AddTransient<GarmentBookingOrderItemLogic>()
                .AddTransient<MonitoringRemainingEHLogic>()
                .AddTransient<WeeklyPlanLogic>()
                .AddTransient<GarmentSewingBlockingPlanLogic>()
                .AddTransient<AcceptedOrderMonitoringLogic>()
                .AddTransient<SewingBlockingPlanReportLogic>()
                .AddTransient<OverScheduleMonitoringLogic>()
                .AddTransient<WeeklyWorkingScheduleMonitoringLogic>()
                .AddTransient<MaxWHConfirmLogic>()
                .AddTransient<GarmentPreSalesContractLogic>();
            
        }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IIdentityService,IdentityService>()
                .AddScoped<IHttpClientService, HttpClientService>()
                .AddScoped<IValidateService,ValidateService>();
        }

        private void RegisterEndpoint()
        {
            Com.Danliris.Service.Sales.WebApi.Utilities. APIEndpoint.Core = Configuration.GetValue<string>("CoreEndpoint") ?? Configuration["CoreEndpoint"];
			Com.Danliris.Service.Sales.WebApi.Utilities.APIEndpoint.AzureCore = Configuration.GetValue<string>("AzureCoreEndpoint") ?? Configuration["AzureCoreEndpoint"];
			Com.Danliris.Service.Sales.Lib.Helpers.APIEndpoint.StorageAccountName = Configuration.GetValue<string>("StorageAccountName") ?? Configuration["StorageAccountName"];
			Com.Danliris.Service.Sales.Lib.Helpers.APIEndpoint.StorageAccountKey = Configuration.GetValue<string>("StorageAccountKey") ?? Configuration["StorageAccountKey"];
            Com.Danliris.Service.Sales.Lib.Helpers.APIEndpoint.AzurePurchasing = Configuration.GetValue<string>("AzurePurchasingEndpoint") ?? Configuration["PurchasingEndpoint"];
            Com.Danliris.Service.Sales.Lib.Helpers.APIEndpoint.Core = Configuration.GetValue<string>("CoreEndpoint") ?? Configuration["CoreEndpoint"];
            Lib.Helpers.APIEndpoint.Production = Configuration.GetValue<string>("ProductionEndpoint") ?? Configuration["ProductionEndpoint"];

        }

        #endregion Register

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            Com.Danliris.Service.Sales.Lib.Helpers.APIEndpoint.ConnectionString = connectionString;
            /* Register */
            services.AddDbContext<SalesDbContext>(options => options.UseSqlServer(connectionString));
            RegisterFacades(services);
            RegisterLogic(services);
            RegisterServices(services);
            RegisterEndpoint();
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
