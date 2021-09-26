using SureSuccess.Shared;
using SureSuccess.Shared.Models;
using SureSuccess.Shared.ViewModels;
using System.Threading.Tasks;

namespace SureSuccess.DeleteSvc.Services
{
    public interface IStudentService
    {
        Task<ResultModel<string>> DeleteStudent(int id);
    }
    public class StudentService : IStudentService
    {
        private readonly IAsyncRepository<Student> _studentRepo;
        public StudentService(IAsyncRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }
       
        public async Task<ResultModel<string>> DeleteStudent(int id)
        {

           var student = await _studentRepo.GetByIdAsync(id);

            if (student == null)
            {
                return new ResultModel<string>($"No student found for id {id}");
            }

            await _studentRepo.DeleteAsync(student);

            return new ResultModel<string>("Student deleted successfully");
        }
    }
}
