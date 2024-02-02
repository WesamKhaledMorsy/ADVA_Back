using ADVA.DB;
using ADVA.Entites_Services.Department_Service;
using ADVA.Entites_Services.Employee_Service;
using ADVA.Repository;
using ADVA.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ADVA.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementPersistence (this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("AdvaConnection")));

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<Constants.Constants, Constants.Constants>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
