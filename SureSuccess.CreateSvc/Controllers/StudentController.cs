using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SureSuccess.CreateSvc.Services;
using SureSuccess.CreateSvc.ViewModels;
using SureSuccess.Shared.AspNetCore;
using SureSuccess.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SureSuccess.CreateSvc.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<StudentDetailsViewModel>), 200)]
        public async Task<IActionResult> CreateStudent(CreateStudentRequestViewModel vm)
        {

            if (!ModelState.IsValid)
                return ApiResponse<StudentDetailsViewModel>(errors: ListModelErrors.ToArray(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _studentService.CreateStudent(vm);

                if (result.HasError)
                    return ApiResponse<StudentDetailsViewModel>(errors: result.ErrorMessages.ToArray());
                return ApiResponse(message: "Successful", codes: ApiResponseCodes.OK, data: result.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }

        }
    }
}
