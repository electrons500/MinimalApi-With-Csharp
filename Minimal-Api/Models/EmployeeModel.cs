namespace Minimal_Api.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int DepartmentId { get; set; }
    }
}
