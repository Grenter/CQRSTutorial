using CQRSTutorial.Cafe.Messaging;
using MassTransit;
using MassTransit.RabbitMqTransport.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ISendEndpointProvider = CQRSTutorial.Cafe.Messaging.ISendEndpointProvider;

namespace CQRSTutorial.Cafe.Web
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
            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(ICommandSender), typeof(CommandSender), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ISendEndpointProvider), typeof(RabbitMqEndpointProvider), ServiceLifetime.Transient));
            var rabbitMqMessageBus = new RabbitMessageBus(new RabbitMqConfiguration());
            services.Add(new ServiceDescriptor(typeof(IBusControl), rabbitMqMessageBus.Create()));
            services.Add(new ServiceDescriptor(typeof(IRabbitMqConfiguration), typeof(RabbitMqConfiguration), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ISendEndpointConfiguration), typeof(RabbitEndpointConfiguration), ServiceLifetime.Transient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
