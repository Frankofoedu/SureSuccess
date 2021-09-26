using Microsoft.AspNetCore.Mvc;
using SureSuccess.Shared.AspNetCore;
using SureSuccess.Shared.ViewModels;
using SureSuccess.UpdateSvc.Services;
using SureSuccess.UpdateSvc.ViewModels;
using System;
using System.Threading.Tasks;

namespace SureSuccess.UpdateSvc.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> UpdateStudentById(int id, UpdateStudentRequestViewModel vm)
        {

            if (id < 1)
                return ApiResponse<string>(errors: $"Id : {id} is not valid", codes: ApiResponseCodes.INVALID_REQUEST);

            if (!ModelState.IsValid)
                return ApiResponse<StudentDetailsViewModel>(errors: ListModelErrors.ToArray(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _studentService.UpdateStudentById(id, vm);

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
