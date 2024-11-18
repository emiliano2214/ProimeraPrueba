using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SecretNight.Repository;
using SecretNight.Services;
using SecretNight.Entities.Data;
using SecretNight.Entities.Models;
using SecretNight.Components;
using System.Text.Json;

namespace SecretNight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar servicios al contenedor
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            // Agregar Swagger para documentación de la API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Empresa API",
                    Version = "v1"
                });
            });

            builder.Services.AddHttpClient();

            // Configurar DbContext para la conexión con la base de datos
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Asegúrate de apuntar a la conexión correcta

            // Registro de repositorios
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

            // Registro de servicios        
            builder.Services.AddScoped<IProductoService, ProductoService>();

            // Configuración de HttpClient para hacer solicitudes a la API
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) });

            // Agregar controladores para las API con opciones de JSON
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            builder.Services.AddEndpointsApiExplorer();

            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor", builder =>
                {
                    builder.WithOrigins("https://localhost:7071") // Asegúrate de que esta URL sea la correcta
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configurar el pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Empresa API v1");
                    c.RoutePrefix = "swagger"; // Esto configurará Swagger en /swagger
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Configurar controladores para manejar las solicitudes de la API
            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Activar CORS
            app.UseCors("AllowBlazor");

            // Configurar los componentes Razor
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(SecretNight.Client._Imports).Assembly);

            app.Run();
        }
    }
}
