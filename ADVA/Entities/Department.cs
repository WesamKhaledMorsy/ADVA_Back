using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADVA.Entities
{
    [Table("Department")]
    public class Department
    {
        public Department() { }
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("FkDepartmentId")]
        public ICollection<Employee> Employees { get; set; }
        public bool IsActive { get; set; }
    }
}
