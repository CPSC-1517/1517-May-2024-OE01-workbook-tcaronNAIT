using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindLibrary.DAL;

namespace WestWindLibrary
{
    public static class WestWindExtensions
    {
        //this is a method of the extension class which will be called from the Program.cs file of the Web App
        //this method does 2 two things
        //  register the context connection string
        //  register ALL services that you create within the BLL layer
        public static void WestWindExtensionServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //handle the connection string
            //adds the context class to the services (IServiceCollection)
            services.AddDbContext<WestWindContext>(options);
        }

    }
}
