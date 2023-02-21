using Microsoft.EntityFrameworkCore;
using Minimal_Api.Models.Data.ApiModels;
using Minimal_Api.Models.Data.UsersManagementDBContext;

namespace Minimal_Api.Models.Data.Service
{
    public class EmployeeService
    {
        private UsersManagementDbContext _context;
        public EmployeeService(UsersManagementDbContext context)
        {
            _context = context;
        }


        public List<EmployeeApiModel> GetEmployees()
        { 
            List<Employee> employees = _context.Employees.Include(x => x.Department)
                                                        .ToList();
            List<EmployeeApiModel> model = employees.Select(x => new EmployeeApiModel
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName
            }).ToList();

            return model;

        }

        public EmployeeApiModel GetEmployeeById(int id)
        {
            Employee employee = _context.Employees.Include(x => x.Department).Where(x => x.EmployeeId == id).FirstOrDefault();
            EmployeeApiModel model = new()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.DepartmentName
            };
            return model;
        }

        public bool AddEmployee(AddEmployeeApiModel model)
        {
            Employee employee = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DepartmentId = model.DepartmentId,
                
            };

            _context.Employees.Add(employee);
            int i = _context.SaveChanges();
            if (i > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            _context.Employees.Remove(employee);
            int i = _context.SaveChanges();
            if (i > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateEmployee(EmployeeModel model)
        {
            Employee employee = _context.Employees.Where(x => x.EmployeeId == model.EmployeeId).FirstOrDefault(); 
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.DepartmentId = model.DepartmentId;
            _context.Employees.Update(employee);
            int i = _context.SaveChanges();
            if (i > 0)
            {
                return true;
            }

            return false;
        }

    }
}
