using ADVA.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADVA.Models
{
    public class EmployeeModel
    {
        public int? Id { get; set; } = null;      
        public string? Name { get; set; } = null;
        public double? Salary { get; set; } = null;

        public int? FkDepartmentId { get; set; } = null;
        public int? FkManagerId { get; set; } = null;
        public ICollection<EmployeeModel>? Employees { get; set; } = null;
        public bool? IsManager { get; set; } = null;
        public bool? IsActive { get; set; } = true;
    }
}
