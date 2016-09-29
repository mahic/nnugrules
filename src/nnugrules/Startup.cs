using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using nnugrules.Middleware;
using nnugrules.Services;
using Serilog;

namespace nnugrules
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .Build();

            var logFile = Path.Combine(env.ContentRootPath, "logfile.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFile)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestCultureOptions>(options =>
            {
                options.DefaultCulture = new CultureInfo(Configuration["culture"] ?? "en-GB");
            });
            services.AddMvc();
            services.AddSingleton<IRequestIdFactory, RequestIdFactory>();
            services.AddScoped<IRequestId, RequestId>();
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<BloggingContext>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient(c => CreateNewContextOptions());

        }

        private static DbContextOptions<BloggingContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<BloggingContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole((category, level) => category == typeof(Startup).FullName);
            loggerFactory.AddSerilog();

            var startupLogger = loggerFactory.CreateLogger<Startup>();

            app.UseRequestId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync($"<strong> You got a {context.Response.StatusCode}<strong>");
                        await context.Response.WriteAsync(new string(' ', 512));  // Padding for IE
                    });
                });

                app.UseExceptionHandler(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("<strong> Application error. Please contact support. </strong>");
                        await context.Response.WriteAsync(new string(' ', 512));  // Padding for IE
                    });
                });
            }
            app.UseRequestCulture();
            app.UseMvc();

            startupLogger.LogInformation("Application startup complete!");

            startupLogger.LogCritical("This is a critical message");
            startupLogger.LogDebug("This is a debug message");
            startupLogger.LogTrace("This is a trace message");
            startupLogger.LogWarning("This is a warning message");
            startupLogger.LogError("This is an error message");
        }
    }
}
