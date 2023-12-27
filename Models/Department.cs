using System;
using System.Collections.Generic;

namespace Day_7.Models;

public partial class Department
{
    public int DeptId { get; set; }

    public string? DeptName { get; set; }

    public string? DeptDesc { get; set; }

    public string? DeptLocation { get; set; }

    public int? DeptManager { get; set; }

    public DateTime? Hiredate { get; set; }

    public virtual Instructor? DeptManagerNavigation { get; set; }
}
