using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;
using NoteApp.Data;
using System.Web.Optimization;

namespace NoteApp
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
            services.AddControllersWithViews();
            services.AddWebOptimizer(pipeline => 
            {
                pipeline.AddCssBundle("/css/bundle.css", "css/**/*.css");                
            });
            services.AddDbContext<NoteDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebOptimizer();
            app.UseStaticFiles();   
            app.UseRouting();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Note}/{action=Index}/{id?}");
            });
        }
    }
}
