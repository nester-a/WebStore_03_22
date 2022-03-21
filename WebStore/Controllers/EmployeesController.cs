using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    [Route("staff")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData employeesData;
        private readonly Logger<EmployeesController> logger;

        public EmployeesController(IEmployeesData employeesData, Logger<EmployeesController> logger)
        {
            this.employeesData = employeesData;
            this.logger = logger;
        }
        [Route("all")]
        public IActionResult Index() => View(employeesData.GetAll());


        [Route("info/{id}")]
        public IActionResult Details(int id)
        {
            var employee = employeesData.GetById(id);
            if(employee is null) return NotFound();
            return View(employee);
        }

    }
}

