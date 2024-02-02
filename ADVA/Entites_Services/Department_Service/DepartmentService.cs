using ADVA.Entities;
using ADVA.Helper;
using ADVA.Models;
using ADVA.Repository;
using ADVA.UnitOfWork;
using AutoMapper;
using Newtonsoft.Json;

namespace ADVA.Entites_Services.Department_Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _IGenericRepository;
        private readonly IUnitOfWork<Department> _IUnitOfWork;
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DepartmentService(IGenericRepository<Department> iGenericRepository, IUnitOfWork<Department> iUnitOfWork, AppDBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _IGenericRepository = iGenericRepository;
            _IUnitOfWork = iUnitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Department GetDepartmentById(int id)
        {
            return _IGenericRepository.GetByIdAsNoTracking(id);
        }
        public string GetAllDepartment(int PageSize, int PageNumber, string? OrderBy, string? SortDir, DepartmentModel model)
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
                param.Add("@DepartmentName", model.Name);
            if (model.ManagerId is not null)
                param.Add("@ManagerId", model.ManagerId);            
            if (model.IsActive is not null)
                param.Add("@IsActive", model.IsActive);
            var result = ConnectionSP.ExcuteStoreProcedure("GetAllDepartment", param);
            var resultserialized = JsonConvert.SerializeObject(result);
            return resultserialized;
        }
        public IQueryable<Department> GetAllDepartment()
        {
            var DepartmentList = _IGenericRepository.GetWithIncludes(x=>x.Employees);
            return DepartmentList;
        }
        public IQueryable<Department> GetAllActiveDepartment()
        {
            var DepartmentList = _IGenericRepository.GetWithIncludes(x => x.Employees).Where(x => x.IsActive == true);
            return DepartmentList;
        }
        public Department InsertDepartment(DepartmentModel department)
        {
            var departmentmap = _mapper.Map<Department>(department);
            var ReqResult = _IGenericRepository.Insert(departmentmap);
            _IUnitOfWork.Save();
            return ReqResult;
        }
        public Department UpdateDepartment(DepartmentModel department)
        {

            var departmentmap = GetDepartmentById((int)department.Id);
            if (departmentmap is null)
            {
                throw new ArgumentException("Id not found");
            }
            department.Id = departmentmap.Id;
            departmentmap = _mapper.Map<Department>(department);
            var ReqResult = _IGenericRepository.Update(departmentmap);
            _IUnitOfWork.Save();
            return ReqResult;
        }
        public int DeleteDepartment(DepartmentModel DDepartment)
        {
            var oneDepartment = GetDepartmentById((int)DDepartment.Id);
            if (oneDepartment is null)
            {
                throw new ArgumentException("Id not found");
            }
            oneDepartment.IsActive = DDepartment.IsActive == false;
            var ReqResult = _IGenericRepository.Update(oneDepartment);
            _IUnitOfWork.Save();
            return ReqResult.Id;
        }
    }
}
