using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StudentsService : IStudentsService
    {
        private readonly List<Student> _students;
        private readonly IRegionsService _regionsService;

        public StudentsService()
        {
            _students= new List<Student>();
            _regionsService= new RegionsService();

        }
        //Helper method that converts Student response and loads the region name at the same time
        private StudentResponse ConvertStudentToStudentResponse(Student student)
        {
            StudentResponse studentResponse = student.ToStudentResponse();
            studentResponse.Region =_regionsService.GetRegionByRegionId(student.RegionId)?.RegionName;
            return studentResponse;
        }

        public StudentResponse AddStudent(StudentAddRequest? studentAddRequest)
        {
            
            if (studentAddRequest == null)
                throw new ArgumentNullException(nameof(studentAddRequest));

            ValidationHelper_Student.ModelValidation(studentAddRequest);


            Student student = studentAddRequest.ToStudent();
            student.StudentId=Guid.NewGuid();
            _students.Add(student);
            return ConvertStudentToStudentResponse(student);
        }

        public List<StudentResponse> GetAllStudents()
        {
            return _students.Select(temp=>temp.ToStudentResponse()).ToList();
        }

        public StudentResponse? GetStudentByStudentId(Guid? studentId)
        {

            if(studentId== null)
                return null;
            Student? student = _students.FirstOrDefault(temp => temp.StudentId==studentId);
            
            if(student == null)
                return null;

            return student.ToStudentResponse();
        }

        public List<StudentResponse> GetFilteredStudents(string searchBy, string? searchValue)
        {
            //Getting all the students and all the matching students | storing said values on a list of
            //StudentResponse type.
            List<StudentResponse> allStudents = GetAllStudents();
            List<StudentResponse> matchingStudents = allStudents;

            //Checking if both search strings are not empty
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchValue))
                return matchingStudents;

            switch (searchBy)
            {
                case nameof(Student.StudentName):
                    matchingStudents = allStudents.Where(temp=>(!string.IsNullOrEmpty(temp.StudentName)?temp.
                    StudentName.Contains(searchValue,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Student.Email):
                    matchingStudents = allStudents.Where(temp => (!string.IsNullOrEmpty(temp.Email)?temp.Email.
                    Contains(searchValue,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Student.DateOfBirth):
                    matchingStudents = allStudents.Where(temp=>(temp.DateOfBirth!=null)?temp.DateOfBirth.Value
                    .ToString("dd MMM yyyy").Contains(searchValue,StringComparison.OrdinalIgnoreCase):true).ToList();
                    break;

                case nameof(Student.Gender):
                    matchingStudents = allStudents.Where(temp=>(!string.IsNullOrEmpty(temp.Gender)?temp.
                    Gender.Contains(searchValue,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Student.RegionId):
                    matchingStudents = allStudents.Where(temp=>(!string.IsNullOrEmpty(temp.Region)?temp.Region
                    .Contains(searchValue,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Student.Nationality):
                    matchingStudents = allStudents.Where(temp=>(!string.IsNullOrEmpty(temp.Nationality)?temp.Nationality
                    .Contains(searchValue,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                default: matchingStudents= allStudents;
                        break;
            }
            return matchingStudents;
        }

        public StudentResponse UpdateStudent(StudentUpdateRequest studentUpdateRequest)
        {
            //if studentupdaterequest is null -> argumentnullexception
            if (studentUpdateRequest == null)
                throw new ArgumentNullException(nameof(Student));

            ValidationHelper_Student.ModelValidation(studentUpdateRequest);

            Student? matchingStudent = _students.FirstOrDefault(temp=>temp.StudentId==studentUpdateRequest.StudentId);

            if (matchingStudent == null)
                throw new ArgumentException("Given student Id does not exist");

            matchingStudent.StudentName = studentUpdateRequest.StudentName;
            matchingStudent.Email = studentUpdateRequest.Email;
            matchingStudent.DateOfBirth = studentUpdateRequest.DateOfBirth;
            matchingStudent.PhoneNumber= studentUpdateRequest.PhoneNumber;
            matchingStudent.Address = studentUpdateRequest.Address;
            matchingStudent.Gender=studentUpdateRequest.Gender.ToString();
            matchingStudent.RegionId= studentUpdateRequest.RegionId;
            matchingStudent.Nationality= studentUpdateRequest.Nationality;

            return matchingStudent.ToStudentResponse();
        }

        public bool DeleteStudent(Guid? studentId)
        {
            if (studentId == null)
                throw new ArgumentNullException(nameof(studentId));

            Student? student = _students.FirstOrDefault(temp=>temp.StudentId==studentId);
            if (student == null)
                return false;

            _students.RemoveAll(temp=>temp.StudentId==studentId);
            return true;
        }
    }
}
