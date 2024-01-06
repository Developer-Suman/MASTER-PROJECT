using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            return services;

        }


        public static IEndpointRouteBuilder AddEndPoint(this IEndpointRouteBuilder builder)
        {
            builder.MapAuthEndPoints();
            return builder;                                                                                        
        }
    }
}
