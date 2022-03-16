using Convertor.API.Contracts;
using Convertor.API.Services.Services;
using Convertor.API.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.API.Services
{
    public static class ServiceExtensions
    {
        public static void AddConvertorAppServices(this IServiceCollection services)
        {
            services.AddScoped<IConvertorService, ConvertorService>();
            services.AddScoped<IConvertProcess, ConvertProcess>();
        }
    }
}
