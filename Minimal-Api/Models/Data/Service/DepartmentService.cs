using Minimal_Api.Models.Data.ApiModels;
using Minimal_Api.Models.Data.UsersManagementDBContext;

namespace Minimal_Api.Models.Data.Service
{
    public class DepartmentService
    {
        private UsersManagementDbContext _context;
        public DepartmentService(UsersManagementDbContext context)
        {
            _context = context;
        }

        public List<DepartmentApiModel> GetDepartments()
        {
            List<Department> departments = _context.Departments.ToList();
            List<DepartmentApiModel> model = departments.Select(x => new DepartmentApiModel
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName
            }).ToList();

            return model;

        }
        
        public DepartmentApiModel GetDepartmentById(int id)
        {
            Department department = _context.Departments.Where(x => x.DepartmentId == id).FirstOrDefault();
            DepartmentApiModel model = new()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
            return model;
        }

        public bool AddDepartment(DepartmentApiModel model)
        {
            Department department = new()
            {
                DepartmentId = model.DepartmentId,
                DepartmentName = model.DepartmentName,
            };

            _context.Departments.Add(department);
           int i = _context.SaveChanges();
            if(i > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteDepartment(int id)
        {
            Department department = _context.Departments.Where(x => x.DepartmentId == id).FirstOrDefault();
            _context.Departments.Remove(department);
            int i = _context.SaveChanges();
            if (i > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateDepartment(DepartmentApiModel model)
        {
            Department department = _context.Departments.Where(x => x.DepartmentId == model.DepartmentId).FirstOrDefault();
            department.DepartmentName = model.DepartmentName;  
            _context.Departments.Update(department);
            int i = _context.SaveChanges();
            if (i > 0)
            {
                return true;
            }

            return false;
        }

    }
}
