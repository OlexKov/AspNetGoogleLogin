using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;



namespace DataAccess
{
    public static class DataAccessServiceExtentions
    {
        public static void AddShopDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(opts =>
                opts.UseNpgsql(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddIdentity<User, UserRole>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //})
            //   .AddDefaultTokenProviders()
            //   .AddEntityFrameworkStores<ShopDbContext>();
        }
    }
}

