using DLL.MongoReport.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.MongoReport.Repositories
{
    public interface IDepartmentStudentMongoRepository
    {
        Task<DepartmentStudentReportMongo> Create(DepartmentStudentReportMongo departmentStudentReportMongo);
        Task<List<DepartmentStudentReportMongo>> GetAll();

    }
    public class DepartmentStudentMongoRepository : IDepartmentStudentMongoRepository
    {
        private readonly MongoDBContext _context;

        public DepartmentStudentMongoRepository(MongoDBContext context)
        {
            _context = context;
        }
         public async Task<DepartmentStudentReportMongo> Create(DepartmentStudentReportMongo departmentStudentReportMongo)
        {
            await _context.DepartmentStudentReport.InsertOneAsync(departmentStudentReportMongo);
            return departmentStudentReportMongo;
        }

        public Task<List<DepartmentStudentReportMongo>> GetAll()
        {
            var filterBuilder = Builders<DepartmentStudentReportMongo>.Filter;
            var filter = filterBuilder.Empty;
            var sort = Builders<DepartmentStudentReportMongo>.Sort.Descending("_id");
            return _context.DepartmentStudentReport.Find(filter).Sort(sort).ToListAsync();
        }
    }
}
