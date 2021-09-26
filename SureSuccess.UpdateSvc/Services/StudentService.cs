using SureSuccess.Shared;
using SureSuccess.Shared.Models;
using SureSuccess.Shared.ViewModels;
using SureSuccess.UpdateSvc.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SureSuccess.UpdateSvc.Services
{
    public interface IStudentService
    {
        Task<ResultModel<StudentDetailsViewModel>> UpdateStudentById(int id, UpdateStudentRequestViewModel viewModel);
    }
    public class StudentService : IStudentService
    {
        private readonly IAsyncRepository<Student> _studentRepo;
        public StudentService(IAsyncRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task<ResultModel<StudentDetailsViewModel>> UpdateStudentById(int id, UpdateStudentRequestViewModel vm)
        {
            var student = await _studentRepo.GetByIdAsync(id);

            if (student == null)
            {
                return new ResultModel<StudentDetailsViewModel>($"No student found for id : {id}");
            }

            student = vm.MapTo(student);

           await _studentRepo.UpdateAsync(student);

            var resultVm = student.MapTo(new StudentDetailsViewModel());

            return new ResultModel<StudentDetailsViewModel>(resultVm);
        }
    }
}
