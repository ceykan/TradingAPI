using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TradingAPI.Business;
using TradingAPI.Core.Caching;
using TradingAPI.Core.Caching.Memory;
using TradingAPI.Core.Caching.Pars.Core.Caching;
using TradingAPI.Core.Caching.Provider;
using TradingAPI.Core.Interfaces;
using TradingAPI.Data.Repositories;
using NSwag;
using NSwag.AspNetCore;

namespace TradingAPI
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
            services.AddMemoryCache();
            services.AddSingleton<IQuotesRepository, QuotesRepository>();
            services.AddSingleton<IQuotesService, QuotesService>();
            services.AddSingleton<ICacheManager>(new CacheManager(new MemoryCacheProvider()));
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
            services.AddControllers();
            services.AddSwaggerDocument(configure => configure.Title = "Trading API");
            services.AddCors(options =>
                             options.AddDefaultPolicy(builder =>
                             builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            app.UseCors();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
