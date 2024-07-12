using Winterhold.DataAccess;
using Winterhold.Presentation.Web.Configurations;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConsole();

            IServiceCollection services = builder.Services;

            Depedencies.ConfigureServices(builder.Configuration, builder.Services);

            services.AddBusinessServices();

            services.AddScoped<AuthorService>();
            services.AddScoped<BookService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<LoanService>();

            // Add services to the container.
            services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}