using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Citd
{
    public class Bootstrap 
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>

            {

                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}