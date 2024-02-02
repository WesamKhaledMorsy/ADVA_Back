using ADVA.Entites_Services.Department_Service;
using ADVA.Entities;
using ADVA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _Constants = ADVA.Constants.Constants;

namespace ADVA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly _Constants _iConstants;
        public DepartmentController(IDepartmentService departmentService, _Constants IConstants)
        {
            _departmentService = departmentService;
            _iConstants = IConstants;
        }


        [HttpPost]
        [Route("getallDepartmentwithpaging")]
        public ActionResult GetAllDepartmentwithpaging(int PageSize, int PageNmuber, string? OrderBy, string? SortDir, DepartmentModel model)
        {
            try
            {
                var Response = _departmentService.GetAllDepartment(PageSize, PageNmuber, OrderBy, SortDir, model);
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
        [Route("getallDepartment")]
        public ActionResult GetAllDepartment()
        {
            try
            {
                var Response = _departmentService.GetAllDepartment();
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
        [Route("getallActiveDepartment")]
        public ActionResult GetAllActiveDepartment()
        {
            try
            {
                var Response = _departmentService.GetAllActiveDepartment();
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
        [Route("getDepartmentbyid")]
        public Department GetDepartmentById(int id)
        {
            return _departmentService.GetDepartmentById(id);
        }


        [HttpPost]
        [Route("insertDepartment")]
        public ActionResult InsertDepartment(DepartmentModel model)
        {
            try
            {

                var response = _departmentService.InsertDepartment(model);
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
        [Route("updateDepartment")]
        public ActionResult UpdateDepartment(DepartmentModel model)
        {
            try
            {
                var Reponse = _departmentService.UpdateDepartment(model);
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
        [Route("deleteDepartment")]
        public ActionResult DeleteDepartment(DepartmentModel model)
        {
            try
            {
                var Reponse = _departmentService.DeleteDepartment(model);
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
