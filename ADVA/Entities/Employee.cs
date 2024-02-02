using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADVA.Entities
{
    [Table("Employee")]
    public class Employee
    {
        public Employee() { }
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public double Salary { get; set; }
        
        public int? FkDepartmentId { get; set; }
        public Department? DepartmentObj { get; set; }
        public int? FkManagerId { get; set; }
        [NotMapped]
        public virtual Employee? ManagerObj { get; set; }
        [ForeignKey("FkManagerId")]
        public ICollection<Employee>? Employees { get; set; }

        public bool IsManager { get; set; }
        public bool IsActive { get; set; }
    }
}
