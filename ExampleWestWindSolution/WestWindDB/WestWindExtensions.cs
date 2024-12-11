using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindDB.BLL;
using WestWindDB.DAL;

namespace WestWindDB
{
	public static class WestWindExtensions
	{
		public static void WestWindExtentionServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
		{
			services.AddDbContext<WestWindContext> (options);

			services.AddScoped<OrderServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetService<WestWindContext>();
				return new OrderServices(context);
			});

            services.AddScoped<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ProductServices(context);
            });

            services.AddScoped<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new CategoryServices(context);
            });

            services.AddScoped<SupplierServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new SupplierServices(context);
            });
        }
	}
}
