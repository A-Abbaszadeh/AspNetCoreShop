using Application.Baskets;
using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.UriComposer;
using Application.Catalogs.GetMenuItem;
using Application.Interfaces.Contexts;
using Application.Users;
using Application.Visitors.OnlineVisitors;
using Application.Visitors.SaveVisitorInfo;
using Infrastructure.IdentityConfigs;
using Infrastructure.MappingProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Contexts;
using Persistence.Contexts.MongoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.EndPoint.Hubs;
using WebSite.EndPoint.Utilities.Filters;
using WebSite.EndPoint.Utilities.Middlewares;
using Application.Orders;
using Application.Payments;
using Application.Discounts;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Orders.CustormerOrderService;
using Application.HomePages;

namespace WebSite.EndPoint
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
            services.AddControllersWithViews();
            services.AddSignalR();


            #region Sql Server
            services.AddTransient<IDatabaseContext, DatabaseContext>();

            string connectionString = Configuration["ConnectionStrings:SqlServer"];
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connectionString));
            #endregion

            #region Identity and Security
            services.AddIdentityService(Configuration);
            services.AddTransient<IIdentityDatabaseContext, IdentityDatabaseContext>();

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
            services.AddTransient<ISaveVisitorInfoService, SaveVisitorInfoService>();
            services.AddTransient<IOnlineVisitorService, OnlineVisitorService>();
            #endregion

            #region Filters
            services.AddScoped<SaveVisitorFilter>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(CatalogMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));
            #endregion

            #region Application
            services.AddTransient<IGetMenuItemService, GetMenuItemService>();
            services.AddTransient<IGetCatalogItemPLPService, GetCatalogItemPLPService>();
            services.AddTransient<IGetCatalogItemPDPService, GetCatalogItemPDPService>();
            var imageServerDomain = Configuration["Domain"];
            services.AddTransient<IUriComposerService>(x => new UriComposerService(imageServerDomain));
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IDiscountHistoryService, DiscountHistoryService>();
            services.AddTransient<ICatalogItemService, CatalogItemService>();
            services.AddTransient<ICustormerOrderService, CustormerOrderService>();
            services.AddTransient<IHomePageService, HomePageService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSetVisitorId();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<OnlineVisitorHub>("/chathub");
            });
        }
    }
}
