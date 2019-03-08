using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace AdorableDog.Rechargeable
{
    public class RechargeableWebTestStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<RechargeableWebTestModule>(options =>
            {
                options.UseAutofac();
            });

            return services.BuildServiceProviderFromFactory();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}