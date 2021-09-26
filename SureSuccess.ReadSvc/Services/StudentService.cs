using SureSuccess.Shared;
using SureSuccess.Shared.Models;
using SureSuccess.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SureSuccess.ReadSvc.Services
{
    public interface IStudentService
    {
        Task<ResultModel<StudentDetailsViewModel>> GetStudentById(int id);
        Task<ResultModel<List<StudentDetailsViewModel>>> GetAllStudents();
    }
    public class StudentService : IStudentService
    {
        private readonly IAsyncRepository<Student> _studentRepo;
        public StudentService(IAsyncRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<ResultModel<List<StudentDetailsViewModel>>> GetAllStudents()
        {
            var students = await _studentRepo.ListAllAsync();

            var vm = students.MapTo(new List<StudentDetailsViewModel>());

            return new ResultModel<List<StudentDetailsViewModel>>(vm);
        }

        public async Task<ResultModel<StudentDetailsViewModel>> GetStudentById(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);

            if (student == null)
            {
                return new ResultModel<StudentDetailsViewModel>($"No student found for id {id}");
            }

            var vm = student.MapTo(new StudentDetailsViewModel());

            return new ResultModel<StudentDetailsViewModel>(vm);
        }
    }
}
