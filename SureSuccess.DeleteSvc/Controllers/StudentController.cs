using Microsoft.AspNetCore.Mvc;
using SureSuccess.DeleteSvc.Services;
using SureSuccess.Shared.AspNetCore;
using SureSuccess.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace SureSuccess.DeleteSvc.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            if (!ModelState.IsValid)
                return ApiResponse<string>(errors: ListModelErrors.ToArray(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _studentService.DeleteStudent(id);

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
