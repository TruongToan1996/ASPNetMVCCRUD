using ASPNetMVCCRUD.Data;
using ASPNetMVCCRUD.Models;
using ASPNetMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDBContext mvcDemoDBContext;

        public EmployeeController(MVCDemoDBContext mvcDemoDBContext)
        {
            this.mvcDemoDBContext = mvcDemoDBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public MVCDemoDBContext GetMvcDemoDBContext()
        {
            return mvcDemoDBContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };


            await mvcDemoDBContext.Employees.AddAsync(employee);
            await mvcDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


    }
}
