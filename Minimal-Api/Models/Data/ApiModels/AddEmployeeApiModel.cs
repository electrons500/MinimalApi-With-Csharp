namespace Minimal_Api.Models.Data.ApiModels
{
    public class AddEmployeeApiModel
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int DepartmentId { get; set; }
    }
}
