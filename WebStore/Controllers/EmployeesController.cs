using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петровчи", Age = 31 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
        };
        public IActionResult Index() => View(_employees);
        public IActionResult Details(int id) //home/details/id
        {
            var employee = _employees.FirstOrDefault(empl => empl.Id == id);
            if(employee == null) return NotFound();
            return View(employee);
        }
    }
}
