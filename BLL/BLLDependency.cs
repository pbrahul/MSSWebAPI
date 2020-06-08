using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using FluentValidation;
using BLL.Request;
using BLL.Services;

namespace BLL
{
    public class BLLDependency
    {
        public static void AllDependency(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "PracticeAPIDB";
            });

            //services.AddTransient<IDepartmentService, DepartmentService>();

            //services.AddTransient<IStudentService, StudentService>();

            services.AddTransient<ITestService, TestService>();

            services.AddTransient<IAccountService, AccountService>();

            //services.AddTransient<ICourseService, CourseService>();

            //services.AddTransient<ICourseEnrollService, CourseEnrollService>();

            //services.AddTransient<IValidator<DepartmentInsertRequest>, DepartmentInsertValidator>();

            //services.AddTransient<IValidator<StudentInsertRequest>, StudentInsertValidator>();

            //services.AddTransient<IValidator<CourseInsertRequest>, CourseInsertValidator>();

            //services.AddTransient<IValidator<CourseEnrollInsertRequest>, CourseEnrollInsertValidator>();

        }
    }
}
