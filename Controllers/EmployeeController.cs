using ASPNetMVCCRUD.Data;
using ASPNetMVCCRUD.Models;
using ASPNetMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDBContext.Employees.ToListAsync();
            return View(employees);
        }

        public MVCDemoDBContext GetMvcDemoDBContext()
        {
            return mvcDemoDBContext;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
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
            return RedirectToAction("Index"); 
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee !=null)
            {
                var viewModel = new UpdateEmployeeViewModel()

                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,

                };
                return await Task.Run(() => View("View", viewModel));
            }
           
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel update)
        {
            var employee =await mvcDemoDBContext.Employees.FindAsync(update.Id);
            if(employee != null)
            {
                employee.Name = update.Name;
                employee.Email = update.Email;
                employee.Salary = update.Salary;
                employee.DateOfBirth = update.DateOfBirth;
                employee.Department = update.Department;

                await mvcDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete (UpdateEmployeeViewModel update)
        {
            var employee = await mvcDemoDBContext.Employees.FindAsync(update.Id);
            if(employee != null)
            {
                mvcDemoDBContext.Remove(employee);
                await mvcDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}
