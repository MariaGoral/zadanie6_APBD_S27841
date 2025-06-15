
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PrescriptionAPI.Models;
using PrescriptionAPI.Services;

namespace PrescriptionAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prescription API", Version = "v1" });
            });
            services.AddDbContext<PrescriptionContext>(opt =>
                opt.UseInMemoryDatabase("PrescriptionDb"));
            services.AddScoped<IPrescriptionService, PrescriptionService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prescription API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
