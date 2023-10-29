using AUTOGLASS.ProductManager.Infra.Contex;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AUTOGLASS.ProductManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductManagerContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();

            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManager", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (serviceScope != null)
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<ProductManagerContext>();
                    context.Database.Migrate();
                }                
            }

            app.UseHttpsRedirection();
            app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        }

    }
}
