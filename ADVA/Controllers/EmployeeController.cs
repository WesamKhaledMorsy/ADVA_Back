using ADVA.Entites_Services.Employee_Service;
using ADVA.Entities;
using ADVA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using _Constants = ADVA.Constants.Constants;

namespace ADVA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly _Constants _iConstants;
        public EmployeeController(IEmployeeService employeeService, _Constants IConstants)
        {
            _employeeService = employeeService;
            _iConstants = IConstants;
        }


        [HttpPost]
        [Route("getallEmployeewithpaging")]
        public ActionResult GetAllEmployeewithpaging(int PageSize, int PageNmuber, string? OrderBy, string? SortDir, EmployeeModel model)
        {
            try
            {
                var Response = _employeeService.GetAllEmployee(PageSize, PageNmuber, OrderBy, SortDir, model);
                string result = _iConstants.GetResponseGenericSuccessFromSpWithPaging(Response);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);

            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }
        [HttpGet]
        [Route("getallEmployee")]
        public ActionResult GetAllEmployee()
        {
            try
            {
                var Response = _employeeService.GetAllEmployee();
                string result = _iConstants.GetResponseGenericSuccess(Response);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);

            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }
        [HttpGet]
        [Route("getallActiveEmployee")]
        public ActionResult GetAllActiveEmployee()
        {
            try
            {
                var Response = _employeeService.GetAllActiveEmployee();
                string result = _iConstants.GetResponseGenericSuccess(Response);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);

            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }


        [HttpGet]
        [Route("getEmployeebyid")]
        public Employee GetEmployeeById(int id)
        {
            return _employeeService.GetEmployeeById(id);
        }


        [HttpPost]
        [Route("insertEmployee")]
        public ActionResult InsertEmployee(EmployeeModel model)
        {
            try
            {

                var response = _employeeService.InsertEmployee(model);
                string result = _iConstants.GetResponseGenericSuccess(response);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }


        [HttpPost]
        [Route("updateEmployee")]
        public ActionResult UpdateEmployee(EmployeeModel model)
        {
            try
            {
                var Reponse = _employeeService.UpdateEmployee(model);
                string result = _iConstants.GetResponseGenericSuccess(Reponse);
                //
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);

                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }

        [HttpPut]
        [Route("deleteEmployee")]
        public ActionResult DeleteEmployee(EmployeeModel model)
        {
            try
            {
                var Reponse = _employeeService.DeleteEmployee(model);
                string result = _iConstants.GetResponseGenericSuccess(Reponse);
                //
                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                string result = _iConstants.GetResponseError(ex.Message);

                return Content(result, _Constants.ContentTypeJson, System.Text.Encoding.UTF8);
            }
        }

    }
}
