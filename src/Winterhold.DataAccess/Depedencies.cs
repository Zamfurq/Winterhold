using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winterhold.DataAccess.Models;

namespace Winterhold.DataAccess
{
    public static class Depedencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services) {
            services.AddDbContext<WinterholdContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WinterholdConnection")));
        }
    }
}
