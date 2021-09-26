using Microsoft.AspNetCore.Mvc;
using SureSuccess.ReadSvc.Services;
using SureSuccess.Shared.AspNetCore;
using SureSuccess.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace SureSuccess.ReadSvc.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> GetStudentById(int id)
        {

            if (id < 1)
                return ApiResponse<string>(errors: $"Id : {id} is not valid", codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _studentService.GetStudentById(id);

                if (result.HasError)
                    return ApiResponse<string>(errors: result.ErrorMessages.ToArray());
                return ApiResponse(message: "Successful", codes: ApiResponseCodes.OK, data: result.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }

        }
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> GetAllStudents()
        {

            try
            {
                var result = await _studentService.GetAllStudents();

                if (result.HasError)
                    return ApiResponse<string>(errors: result.ErrorMessages.ToArray());
                return ApiResponse(message: "Successful", codes: ApiResponseCodes.OK, data: result.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }

        }
    }
}
