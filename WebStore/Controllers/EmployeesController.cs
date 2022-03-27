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

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit
        public IActionResult Edit(int? id)
        {
            
            if(id is null) return View(new EmployeeViewModel());

            var employee = employeesData.GetById((int)id);
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
            if (model.LastName == "Асама" && model.FirstName == "Бин" && model.Patronymic == "Ладан")
                ModelState.AddModelError("", "Террористов не берём");
            if (!ModelState.IsValid) return View(model);
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
            };

            if (employee.Id == 0) employeesData.Add(employee);
            else employeesData.Update(employee);

            return RedirectToAction(nameof(Index));
            
        }
        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var employee = employeesData.GetById(id);
            if (employee is null) return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            });
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}

