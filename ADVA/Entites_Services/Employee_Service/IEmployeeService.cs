using ADVA.Entities;
using ADVA.Models;

namespace ADVA.Entites_Services.Employee_Service
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int id);
        string GetAllEmployee(int PageSize, int PageNumber, string? OrderBy, string? SortDir, EmployeeModel model);
        IQueryable<Employee> GetAllEmployee();
        IQueryable<Employee> GetAllActiveEmployee();
        Employee InsertEmployee(EmployeeModel employee);
        Employee UpdateEmployee(EmployeeModel employee);
        int DeleteEmployee(EmployeeModel DEmployee);
    }
}
