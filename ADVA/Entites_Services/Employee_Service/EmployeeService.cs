using ADVA.Entities;
using ADVA.Helper;
using ADVA.Models;
using ADVA.Repository;
using ADVA.UnitOfWork;
using AutoMapper;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ADVA.Entites_Services.Employee_Service
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IGenericRepository<Employee> _IGenericRepository;
        private readonly IUnitOfWork<Employee> _IUnitOfWork;
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> iGenericRepository, IUnitOfWork<Employee> iUnitOfWork, AppDBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _IGenericRepository = iGenericRepository;
            _IUnitOfWork = iUnitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Employee GetEmployeeById(int id)
        {
            return _IGenericRepository.GetByIdAsNoTracking(id);
        }
        public string GetAllEmployee(int PageSize, int PageNumber, string? OrderBy, string? SortDir, EmployeeModel model)
        {
            var param = new Dictionary<string, object>();
            param.Add("@PageSize", PageSize);
            param.Add("@PageNumber", PageNumber);
            if (OrderBy is not null)
                param.Add("@OrderBy", OrderBy);
            if (SortDir is not null)
                param.Add("@SortDir", SortDir);
            if (model.Id is not null)
                param.Add("@Id", model.Id);
            if (model.Name is not null)
                param.Add("@EmployeeName", model.Name);
            if (model.Salary is not null)
                param.Add("@EmployeeSalary", model.Salary);
            if (model.IsManager is not null)
                param.Add("@IsManager", model.IsManager);
            if (model.FkManagerId is not null)
                param.Add("@FkManagerId", model.FkManagerId);
            if (model.FkDepartmentId is not null)
                param.Add("@FkDepartmentId", model.FkDepartmentId);
            if (model.IsActive is not null)
                param.Add("@IsActive", model.IsActive);
            var result = ConnectionSP.ExcuteStoreProcedureReturnThree("GetAllEmployee", param);
            var resultserialized = JsonConvert.SerializeObject(result);
            return resultserialized;            
        }
        public IQueryable<Employee> GetAllEmployee()
        {
            var EmployeeList = _IGenericRepository.GetAll();
            return EmployeeList;
        }
        public IQueryable<Employee> GetAllActiveEmployee()
        {
            var EmployeeList = _IGenericRepository.GetAll().Where(x => x.IsActive == true);
            return EmployeeList;
        }
        public Employee InsertEmployee(EmployeeModel employee)
        {
            var employeemap = _mapper.Map<Employee>(employee);
            var ReqResult = _IGenericRepository.Insert(employeemap);
            _IUnitOfWork.Save();
            return ReqResult;
        }
        public Employee UpdateEmployee(EmployeeModel employee)
        {

            var employeemap = GetEmployeeById((int)employee.Id);
            if (employeemap is null)
            {
                throw new ArgumentException("Id not found");
            }               
            employee.Id = employeemap.Id;
            employeemap = _mapper.Map<Employee>(employee);
            var ReqResult = _IGenericRepository.Update(employeemap);
            _IUnitOfWork.Save();
            return ReqResult;
        }
        public int DeleteEmployee(EmployeeModel DEmployee)
        {
            var oneEmployee = GetEmployeeById((int)DEmployee.Id);
            if (oneEmployee is null)
            {
                throw new ArgumentException("Id not found");
            }
            oneEmployee.IsActive = DEmployee.IsActive == false;
            var ReqResult = _IGenericRepository.Update(oneEmployee);
            _IUnitOfWork.Save();
            return ReqResult.Id;
        }
    }
}
