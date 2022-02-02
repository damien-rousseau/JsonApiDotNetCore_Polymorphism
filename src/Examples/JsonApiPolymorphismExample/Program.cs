using JsonApiDotNetCore.Configuration;
using JsonApiPolymorphismExample.Managers;
using JsonApiPolymorphismExample.Managers.Contracts;
using JsonApiPolymorphismExample.Models;
using JsonApiPolymorphismExample.Services;
using JsonApiPolymorphismExample.Services.Contracts;
using Microsoft.OpenApi.Models;

namespace WebApplication23
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

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

            services.AddJsonApi(discovery => discovery.DefaultPageSize = new PageSize(10)
                , facade => facade.AddCurrentAssembly());

            services.AddScoped<IService<ArticleBase>, ArticleService>();
            services.AddScoped<IConstraintsManager, ConstraintsManager>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication23", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication23 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseJsonApi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddJsonApi(discovery => discovery.DefaultPageSize = new PageSize(10)
//    , facade => facade.AddCurrentAssembly());

//builder.Services.AddScoped<IService<ArticleBase>, ArticleService>();
//builder.Services.AddScoped<IConstraintsManager, ConstraintsManager>();

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.UseJsonApi();

//app.MapControllers();

//app.Run();
