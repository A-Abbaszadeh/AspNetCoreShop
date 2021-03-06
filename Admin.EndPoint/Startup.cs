using Admin.EndPoint.MappingProfiles;
using Application.Banners;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.CatalogTypes;
using Application.Discounts;
using Application.Discounts.AddNewDiscountService;
using Application.Interfaces.Contexts;
using Application.UriComposer;
using Application.Visitors.GetTodayReport;
using Application.Visitors.OnlineVisitors;
using FluentValidation;
using Infrastructure.ExternalApi.ImageServer;
using Infrastructure.IdentityConfigs;
using Infrastructure.MappingProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Contexts;
using Persistence.Contexts.MongoContext;
using System;

namespace Admin.EndPoint
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
            services.AddRazorPages();
            services.AddControllers();

            #region Sql Server
            string connectionString = Configuration["ConnectionStrings:SqlServer"];
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            services.AddDistributedSqlServerCache(option =>
            {
                option.ConnectionString = connectionString;
                option.SchemaName = "dbo";
                option.TableName = "CacheData";
            });
            #endregion

            #region Identity and Security
            services.AddIdentityService(Configuration);

            services.AddAuthorization();
            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                option.LoginPath = "/account/login";
                option.AccessDeniedPath = "/account/accessdenied";
                option.SlidingExpiration = true;
            });
            #endregion

            #region MongoDb Services
            services.AddTransient(typeof(IVisitorDbContext<>), typeof(VisitorDbContext<>));
            services.AddTransient<IGetTodayReportService, GetTodayReportService>();
            services.AddTransient<IOnlineVisitorService, OnlineVisitorService>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(CatalogMappingProfile));
            services.AddAutoMapper(typeof(CatalogVMMappingProgile));
            #endregion

            #region Application
            services.AddTransient<ICatalogTypeService, CatalogTypeService>();
            services.AddTransient<IAddNewCatalogItemService, AddNewCatalogItemService>();
            services.AddTransient<ICatalogItemService, CatalogItemService>();
            services.AddTransient<IImageUploadService, ImageUploadService>();
            services.AddTransient<IAddNewDiscountService, AddNewDiscountService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IDiscountHistoryService, DiscountHistoryService>();
            services.AddTransient<IBannerService, BannerService>();
            var imageServerDomain = Configuration["Domain"];
            services.AddTransient<IUriComposerService>(x => new UriComposerService(imageServerDomain));

            #endregion

            #region Fluent validation
            services.AddTransient<IValidator<AddNewCatalogItemDto>, AddNewCatalogItemDtoValidator>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
