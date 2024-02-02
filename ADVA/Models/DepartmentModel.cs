using ADVA.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADVA.Models
{
    public class DepartmentModel
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public int? ManagerId { get; set; }=null;
        public ICollection<EmployeeModel>? Employees { get; set; }= null;
        public bool? IsActive { get; set; } = null; 
    }
}
