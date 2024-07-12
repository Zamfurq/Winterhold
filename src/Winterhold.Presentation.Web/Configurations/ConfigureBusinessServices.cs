using Winterhold.Business.Interfaces;
using Winterhold.Business.Repositories;

namespace Winterhold.Presentation.Web.Configurations
{
    public static class ConfigureBusinessServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            return services;
        }
    }
}
