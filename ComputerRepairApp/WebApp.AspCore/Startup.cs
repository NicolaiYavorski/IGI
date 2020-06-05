using Autofac;
using BusinessLogic.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.AspCore.Infrastructure;
using WebApp.AspCore.Interfaces;
using WebApp.AspCore.Services;

namespace WebApp.AspCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("IdentityContext");
            services.AddDbContext<IdentityAppContext>(options => options.UseSqlServer(connectionString));

              services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityAppContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            services.AddControllersWithViews();
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(option => option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"));

            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = Configuration.GetConnectionString("ComputerConnection");
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .Options;

            builder.RegisterModule(new BllModule(options));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
