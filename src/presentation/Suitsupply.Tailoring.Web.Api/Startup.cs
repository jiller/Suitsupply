using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using Suitsupply.Tailoring.Web.Api.DependencyInjection;
using Suitsupply.Tailoring.Web.Api.HostedServices;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Suitsupply.Tailoring.Web.Api
{
    public class Startup
    {
        private readonly TailoringAppContext _context;

        public Startup(IConfiguration configuration)
        {
            _context = new TailoringAppContext(configuration);
        }

        private void OnShutdown()
        {
            _context.Dispose();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddLogging(builder => { builder.AddApplicationInsights(); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSpaStaticFiles(options => options.RootPath = "app");
            
            services.AddSimpleInjector(_context.Container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();

                options.AddHostedService<QueueListenerService>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSimpleInjector(_context.Container, options =>
            {
                options.AutoCrossWireFrameworkComponents = true;
                options.UseLogging();
                options.CrossWire<IServiceScopeFactory>();
            });
            
            _context.InitializeContainer();
            _context.Container.Verify();
            
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseMvc();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseSpa(builder =>
            {
                builder.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    builder.UseAngularCliServer(npmScript: "start");
                }
            });
            
            // Register shutdown handler to safe dispose the DI container
            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }
    }
}
