using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WmIdentity.Models;
using WmIdentity.Services;



namespace WmIdentity
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
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<WmIdentityDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

           

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });           


            
       
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IRepository<Product>, BaseRepository<Product>>();
            //services.AddScoped<IRepository<Category>, BaseRepository<Category>>();
            //services.AddScoped<IRepository<Photo>, BaseRepository<Photo>>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddSingleton<IEmailSender, EmailSender>();


            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR")
                };

                opts.DefaultRequestCulture = new RequestCulture("pt-br");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });




            //string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=WmIdentity.MyUser;trusted_connection=yes;";

            services.AddMvc().
              AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddIdentity<MyUser, IdentityRole>(options => {

                //options.Tokens.EmailConfirmationTokenProvider = "emailconf";
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
                
            })
                .AddEntityFrameworkStores<WmIdentityDbContext>()
                .AddDefaultTokenProviders()
                //.AddTokenProvider<EmailConfirmationTokenProvider<MyUser>>("emailconf")
                .AddPasswordValidator<DoesNotContainPasswordValidator<MyUser>>()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>(); // Add this line
            

            //services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(3));
            //services.Configure<EmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(20));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");


           

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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);
            var supportedCultures = new[]
            {
                new CultureInfo("pt-BR"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseCookiePolicy();

            //app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
