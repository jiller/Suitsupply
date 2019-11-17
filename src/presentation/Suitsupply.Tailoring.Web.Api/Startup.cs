using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Suitsupply.Tailoring.Web.Api.DependencyInjection;
using Suitsupply.Tailoring.Web.Api.HostedServices;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Suitsupply.Tailoring.Web.Api
{
    public class Startup
    {
        private readonly TailoringAppContext _context;

        public Startup(IHostingEnvironment environment, IApplicationLifetime applicationLifetime, IConfiguration configuration)
        {
            _context = new TailoringAppContext(configuration);

            // Register shutdown handler to safe dispose the DI container
            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            _context.Dispose();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLogging();
            
            services.AddSimpleInjector(_context.Container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();

                options.AddHostedService<QueueListenerService>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSimpleInjector(_context.Container, options => { });
            app.UseMvc();
        }
    }
}
