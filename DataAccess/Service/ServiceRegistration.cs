using DataAccess.Data;
using DataAccess.IRipository.Cart;
using DataAccess.IRipository.IProducts;
using DataAccess.IRipository.IUser;
using DataAccess.Ripository.Cart;
using DataAccess.Ripository.Login;
using DataAccess.Ripository.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public static class ServiceRegistration
    {
        public static void AddLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<IRegister, RegisterRepository>();
            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<IProduct, ProductRipository>();
            services.AddScoped<ICart, CartRepository>();
            services.AddScoped<IOrder, OrderRepository>();
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
        }
    }
}
