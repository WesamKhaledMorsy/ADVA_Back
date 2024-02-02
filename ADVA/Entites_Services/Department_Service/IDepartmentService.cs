using ADVA.Entities;
using ADVA.Models;

namespace ADVA.Entites_Services.Department_Service
{
    public interface IDepartmentService
    {
        Department GetDepartmentById(int id);
        string GetAllDepartment(int PageSize, int PageNumber, string? OrderBy, string? SortDir, DepartmentModel model);
        IQueryable<Department> GetAllDepartment();
        IQueryable<Department> GetAllActiveDepartment();
        Department InsertDepartment(DepartmentModel department);
        Department UpdateDepartment(DepartmentModel employee);
        int DeleteDepartment(DepartmentModel DDepartment);
    }
}
