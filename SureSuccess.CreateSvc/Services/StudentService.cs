using SureSuccess.CreateSvc.ViewModels;
using SureSuccess.Shared;
using SureSuccess.Shared.Models;
using SureSuccess.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SureSuccess.CreateSvc.Services
{
    public interface IStudentService
    {
        Task<ResultModel<StudentDetailsViewModel>> CreateStudent(CreateStudentRequestViewModel vm);
    }
    public class StudentService : IStudentService
    {
        private readonly IAsyncRepository<Student> _studentRepo;
        public StudentService(IAsyncRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }
       
        public async Task<ResultModel<StudentDetailsViewModel>> CreateStudent(CreateStudentRequestViewModel vm)
        {
            var student = vm.MapTo(new Student());

           await _studentRepo.AddAsync(student);

            var resultModel = student.MapTo(new StudentDetailsViewModel());

            return new ResultModel<StudentDetailsViewModel>(resultModel);
        }
    }
}
