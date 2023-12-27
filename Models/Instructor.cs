using System;
using System.Collections.Generic;

namespace Day_7.Models;

public partial class Instructor
{
    public int InsId { get; set; }

    public string? InsName { get; set; }

    public string? InsDegree { get; set; }

    public int? DeptId { get; set; }

    public int? Salary { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
