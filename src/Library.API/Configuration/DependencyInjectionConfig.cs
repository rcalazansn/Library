using Library.Core.Notification;
using Library.Domain.Repositories;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection")));

            //Core
            services.AddScoped<INotifier, Notifier>();

            //Infrastructure
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repository
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
