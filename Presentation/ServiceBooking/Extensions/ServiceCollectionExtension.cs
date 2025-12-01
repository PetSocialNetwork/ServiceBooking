using FluentValidation;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ServiceBooking.WebApi.Filters;
using ServiceBooking.Domain.Interfaces;
using ServiceBooking.DataEntityFramework;
using ServiceBooking.DataEntityFramework.Repositories;
using ServiceBooking.Domain.Services;

namespace ServiceBooking.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddApplicationComponents(configuration);
        }

        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddControllers(options =>
            {
                options.Filters.Add<CentralizedExceptionHandlingFilter>();
            });

            services.AddFluentValidationAutoValidation();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PetCareService", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void AddApplicationComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddDomainServices();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<BookingService>();
        }
    }
}
