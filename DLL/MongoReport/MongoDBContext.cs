using DLL.MongoReport.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.MongoReport
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase database;

        public MongoDBContext(IConfiguration configaration)
        {
            var client = new MongoClient(configaration.GetValue<string>("MongoConnection:ConnectionString"));
            database = client.GetDatabase(configaration.GetValue<string>("MongoConnection:Database"));
        }

        public IMongoCollection<DepartmentStudentReportMongo> DepartmentStudentReport =>
            database.GetCollection<DepartmentStudentReportMongo>("DepartmentStudent");
    }
}
