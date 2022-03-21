using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("staff")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData employeesData;
        private readonly ILogger<EmployeesController> logger;

        public EmployeesController(IEmployeesData employeesData, ILogger<EmployeesController> logger)
        {
            this.employeesData = employeesData;
            this.logger = logger;
        }

        //[Route("all")]
        public IActionResult Index() => View(employeesData.GetAll());


        //[Route("info/{id}")]
        public IActionResult Details(int id)
        {
            var employee = employeesData.GetById(id);
            if(employee is null) return NotFound();
            return View(employee);
        }

        public IActionResult Create() => View();

        #region Edit
        public IActionResult Edit(int id)
        {
            var employee = employeesData.GetById(id);
            if(employee is null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age= model.Age,
            };

            employeesData.Update(employee);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        public IActionResult Delete(int id) => View();
    }
}

