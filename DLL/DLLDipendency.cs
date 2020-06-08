using DLL.MongoReport;
using DLL.Repository;
using DLL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL
{
    public class DLLDipendency
    {
        public static void Alldependency(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            //services.AddTransient<IStudentRepository, StudentRepository>();
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            MonfoDBDependency.Alldependency(services);
        }

    }
}
