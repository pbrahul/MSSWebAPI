using DLL.MongoReport.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.MongoReport
{
    public class MonfoDBDependency
    {
        public static void Alldependency(IServiceCollection services)
        {
            services.AddSingleton(typeof(MongoDBContext));
            services.AddTransient <IDepartmentStudentMongoRepository, DepartmentStudentMongoRepository> ();
        }
    }
    
}
