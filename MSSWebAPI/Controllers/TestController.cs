using System.Threading.Tasks;
using BLL.Services;
using DLL.MongoReport.Models;
using DLL.MongoReport.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSSWebAPI.Controllers;
using Utility.Helpers;

namespace MSSWebAPI.Controllers
{
    public class TestController : APIConnectionController
    {
        private readonly ITestService _testService;
        private readonly TaposRSA _taposRSA;
        private readonly IDepartmentStudentMongoRepository _departmentStudentMongoRepository;

        public TestController(ITestService testService, TaposRSA taposRSA, IDepartmentStudentMongoRepository departmentStudentMongoRepository)
        {
            _testService = testService;
            _taposRSA = taposRSA;
            _departmentStudentMongoRepository = departmentStudentMongoRepository;
        }

        //[HttpPost]
        ////[System.Obsolete]
        //public async Task<IActionResult> Index()
        //{
        //    _taposRSA.GenerateRsaKey("v1");
        //    await _testService.SaveAllData();
        //    return Ok("Hello");

        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //_taposRSA.GenerateRsaKey("v1");
            //await _testService.SaveAllData();
            //await _testService.UpdateBalance();
            await _departmentStudentMongoRepository.Create(new DepartmentStudentReportMongo()
            {
                DepartmentCode = "CSE",
                DepartmentName="Computer Science & Engineering",
                StudentEmail="rahul@gmail.com",
                StudentRollNo="0010",
                StudentName="Rahul"
            });
           
            return Ok("Hello");

        }
        [HttpGet(template: "all")]
        public async Task<IActionResult> All()
        {
            return Ok(await _departmentStudentMongoRepository.GetAll());
        }
        }
    }