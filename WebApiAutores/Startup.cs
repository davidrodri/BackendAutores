using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebApiAutores
{
    public class Startup      
    {
        public Startup(IConfiguration configuration) { 
            Configuration =configuration;
        }

        public IConfiguration Configuration {  get; }
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers().AddJsonOptions(
                x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "WebApiAutores", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiAutores v1"));
            }

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