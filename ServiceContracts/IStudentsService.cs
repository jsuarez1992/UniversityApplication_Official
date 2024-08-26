using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IStudentsService
    {
        StudentResponse AddStudent(StudentAddRequest? studentAddRequest);
        List<StudentResponse> GetAllStudents();
        StudentResponse? GetStudentByStudentId(Guid? studentId);
        List<StudentResponse> GetFilteredStudents(string seachBy, string? searchValue);
        StudentResponse UpdateStudent(StudentUpdateRequest studentUpdateRequest);
        bool DeleteStudent(Guid? studentId);
    }
}
