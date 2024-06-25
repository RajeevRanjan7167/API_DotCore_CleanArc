using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<LeaveManagementDbContext>(options =>
            options.UseSqlServer(
                    configuration.GetConnectionString("LeaveManagementConnectionString"))
            );

            serviceCollection.AddScoped(typeof(IGenericRepsitory<>), typeof(GenericRepsitory<>));

            serviceCollection.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            serviceCollection.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            serviceCollection.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return serviceCollection;
        }
    }
}
