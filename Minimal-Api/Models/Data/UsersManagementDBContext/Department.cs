using System;
using System.Collections.Generic;

namespace Minimal_Api.Models.Data.UsersManagementDBContext;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
