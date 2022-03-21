using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("staff")]
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<Employee> _employees;

        public EmployeesController()
        {
            _employees = TestData.Employees;
        }
        [Route("all")]
        public IActionResult Index() => View(_employees);


        [Route("info/{id}")]
        public IActionResult Details(int id)
        {
            var employee = _employees.SingleOrDefault(empl => empl.Id == id);
            if(employee is null) return NotFound();
            return View(employee);
        }

    }
}

