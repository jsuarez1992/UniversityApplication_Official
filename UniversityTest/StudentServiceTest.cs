using Entities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UniversityTest
{
    public class StudentServiceTest
    {
        private readonly IStudentsService _studentService;
        private readonly IRegionsService _regionsService;
        private readonly ITestOutputHelper _testOutputHelper;
        public StudentServiceTest(ITestOutputHelper testOutputHelper)
        {
            _studentService = new StudentsService();
            _regionsService = new RegionsService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddStudent()
        //when adding null obj -> null on return
        [Fact]
        public void AddStudent_NullStudent()
        {
            StudentAddRequest? new_student = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _studentService.AddStudent(new_student);
            });
        }

        //when null value gets supplied as formname -> ArgumentException
        [Fact]
        public void AddStudent_NullStudentName()
        {
            StudentAddRequest new_student_name = new StudentAddRequest() { StudentName = null };

            Assert.Throws<ArgumentException>(() =>
            {
                _studentService.AddStudent(new_student_name);
            });
        }

        //when all values are added -> proper adding
        [Fact]
        public void AddStudent_ProperStudentAdd()
        {
            StudentAddRequest? student_addreq = new StudentAddRequest()
            {
                StudentName = "Marja Attinen",
                Email =
            "marja.attinen@example.com",
                PhoneNumber = "043-969-4512",
                DateOfBirth = DateTime.Parse("1990-06-27"),
                Address = "Keskustantie 18 A12",
                Gender = GenderOptions.Female,
                RegionId = Guid.NewGuid(),
                Nationality =
            "Dominican"
            };

            StudentResponse student_from_addreq = _studentService.AddStudent(student_addreq);
            List<StudentResponse> student_to_list = _studentService.GetAllStudents();
            Assert.True(student_from_addreq.StudentId != Guid.Empty);
            Assert.Contains(student_from_addreq, student_to_list);
        }
        #endregion

        #region GetStudentByStudentId()
        //Requirement 1: If we supply a null value as a Person ID, the PersonResponse? object should also be null.
        [Fact]
        public void GetStudentByStudentId_NullIdValue()
        {
            Guid? studentId = null;

            StudentResponse? student_Id_from_get = _studentService.GetStudentByStudentId(studentId);
            Assert.Null(student_Id_from_get);
        }

        //Requirement 2: If we supply a valid person ID, then it should return the proper value as a PersonResponse? object.
        [Fact]
        public void GetStudentByStudentId_ProperIdReturn()
        {
            RegionAddRequest? region_request = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionResponse region_response = _regionsService.AddRegion(region_request);

            StudentAddRequest? student_request = new StudentAddRequest()
            {
                StudentName = "Pablo Escobar",
                Email = "pablo_esc@gmail.com",
                PhoneNumber = "045-236-486",
                DateOfBirth = DateTime.Parse("1995-11-26"),
                Address = "St. Lauren 123",
                Gender = GenderOptions.Male,
                RegionId = region_response.RegionId,
                Nationality = "Colombian"
            };

            StudentResponse student_from_add = _studentService.AddStudent(student_request);
            StudentResponse? student_from_get = _studentService.GetStudentByStudentId(student_from_add.StudentId);
            Assert.Equal(student_from_add, student_from_get);
        }

        #endregion


        #region GetAllStudents()
        //The GetAllPersons() method should return an empty list by default
        [Fact]
        public void GetAllStudents_EmptyList()
        {
            List<StudentResponse> students_from_get = _studentService.GetAllStudents();
            Assert.Empty(students_from_get);
        }

        //After adding a few persons and then calling GetAllPersons(),
        //it should return the same persons that we added before.
        [Fact]
        public void GetAllStudents_ProperAddReturn()
        {
            RegionAddRequest? region_addreq_1 = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionAddRequest? region_addreq_2 = new RegionAddRequest() { RegionName = "Iisalmi" };

            RegionResponse? region_from_add_1 = _regionsService.AddRegion(region_addreq_1);
            RegionResponse? region_from_add_2 = _regionsService.AddRegion(region_addreq_2);

            StudentAddRequest? student_request_1 = new StudentAddRequest()
            {
                StudentName = "Pablo Escobar",
                Email = "pablo_esc@gmail.com",
                PhoneNumber = "045-236-486",
                DateOfBirth = DateTime.Parse("1995-11-26"),
                Address = "St. Lauren 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_1.RegionId,
                Nationality = "Colombian"
            };
            StudentAddRequest? student_request_2 = new StudentAddRequest()
            {
                StudentName = "Mila Kunis",
                Email = "mkunis@gmail.com",
                PhoneNumber = "056-456-954",
                DateOfBirth = DateTime.Parse("1998-07-18"),
                Address = "Random Street 456",
                Gender = GenderOptions.Female,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Russian"
            };

            StudentAddRequest? student_request_3 = new StudentAddRequest()
            {
                StudentName = "Anttu Pekkanen",
                Email = "eramos@outlook.com",
                PhoneNumber = "658-978-245",
                DateOfBirth = DateTime.Parse("1997-10-09"),
                Address = "Random Alley 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Finnish"
            };

            List<StudentAddRequest> studentAddRequests = new List<StudentAddRequest>(){student_request_1,student_request_2,
            student_request_3};

            List<StudentResponse> student_list_from_add = new List<StudentResponse>();

            foreach (StudentAddRequest? student in studentAddRequests)
            {
                StudentResponse studentResponse = _studentService.AddStudent(student);
                student_list_from_add.Add(studentResponse);
            }
            //Directive to print expected values
            _testOutputHelper.WriteLine("Expected: ");
            foreach(StudentResponse student_response_from_add in student_list_from_add)
            {
                //Printing values for a more detail test
                _testOutputHelper.WriteLine(student_response_from_add.ToString());
            }

            List<StudentResponse> student_list_from_get = _studentService.GetAllStudents();

            //Directive to print expected values
            _testOutputHelper.WriteLine("Actual: ");
            foreach(StudentResponse student_response_from_get in student_list_from_get)
            {
                _testOutputHelper.WriteLine(student_response_from_get.ToString());
            }

            foreach (StudentResponse student_response_from_add in student_list_from_add)
            {
                Assert.Contains(student_response_from_add, student_list_from_get);
            }           


        }

        #endregion

        #region GetFilteredStudents()

        //If searchValue is empty but searchBy= PersonName -> returns all person matching searchBy
        [Fact]
        public void GetFilteredStudents_EmtySeachText()
        {
            RegionAddRequest? region_addreq_1 = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionAddRequest? region_addreq_2 = new RegionAddRequest() { RegionName = "Iisalmi" };

            RegionResponse? region_from_add_1 = _regionsService.AddRegion(region_addreq_1);
            RegionResponse? region_from_add_2 = _regionsService.AddRegion(region_addreq_2);

            StudentAddRequest? student_request_1 = new StudentAddRequest()
            {
                StudentName = "Pablo Escobar",
                Email = "pablo_esc@gmail.com",
                PhoneNumber = "045-236-486",
                DateOfBirth = DateTime.Parse("1995-11-26"),
                Address = "St. Lauren 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_1.RegionId,
                Nationality = "Colombian"
            };
            StudentAddRequest? student_request_2 = new StudentAddRequest()
            {
                StudentName = "Mila Kunis",
                Email = "mkunis@gmail.com",
                PhoneNumber = "056-456-954",
                DateOfBirth = DateTime.Parse("1998-07-18"),
                Address = "Random Street 456",
                Gender = GenderOptions.Female,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Russian"
            };

            StudentAddRequest? student_request_3 = new StudentAddRequest()
            {
                StudentName = "Anttu Pekkanen",
                Email = "eramos@outlook.com",
                PhoneNumber = "658-978-245",
                DateOfBirth = DateTime.Parse("1997-10-09"),
                Address = "Random Alley 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Finnish"
            };

            List<StudentAddRequest> studentAddRequests = new List<StudentAddRequest>(){student_request_1,student_request_2,
            student_request_3};

            List<StudentResponse> student_list_from_add = new List<StudentResponse>();

            foreach (StudentAddRequest? student in studentAddRequests)
            {
                StudentResponse studentResponse = _studentService.AddStudent(student);
                student_list_from_add.Add(studentResponse);
            }
            //Directive to print expected values
            _testOutputHelper.WriteLine("Expected: ");
            foreach (StudentResponse student_response_from_add in student_list_from_add)
            {
                //Printing values for a more detail test
                _testOutputHelper.WriteLine(student_response_from_add.ToString());
            }

            List<StudentResponse> student_list_from_search = _studentService.GetFilteredStudents(nameof(Student.StudentName),"");

            //Directive to print expected values
            _testOutputHelper.WriteLine("Actual: ");
            foreach (StudentResponse student_response_from_get in student_list_from_search)
            {
                _testOutputHelper.WriteLine(student_response_from_get.ToString());
            }

            foreach (StudentResponse student_response_from_add in student_list_from_add)
            {
                Assert.Contains(student_response_from_add, student_list_from_search);
            }

        }

        //We add a few people, then we will search based on a searchValue -> return matching persons
        [Fact]
        public void GetFilteredStudents_SearchByName()
        {
            RegionAddRequest? region_addreq_1 = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionAddRequest? region_addreq_2 = new RegionAddRequest() { RegionName = "Iisalmi" };

            RegionResponse? region_from_add_1 = _regionsService.AddRegion(region_addreq_1);
            RegionResponse? region_from_add_2 = _regionsService.AddRegion(region_addreq_2);

            StudentAddRequest? student_request_1 = new StudentAddRequest()
            {
                StudentName = "Pablo Escobar",
                Email = "pablo_esc@gmail.com",
                PhoneNumber = "045-236-486",
                DateOfBirth = DateTime.Parse("1995-11-26"),
                Address = "St. Lauren 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_1.RegionId,
                Nationality = "Colombian"
            };
            StudentAddRequest? student_request_2 = new StudentAddRequest()
            {
                StudentName = "Mila Kunis",
                Email = "mkunis@gmail.com",
                PhoneNumber = "056-456-954",
                DateOfBirth = DateTime.Parse("1998-07-18"),
                Address = "Random Street 456",
                Gender = GenderOptions.Female,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Russian"
            };

            StudentAddRequest? student_request_3 = new StudentAddRequest()
            {
                StudentName = "Anttu Pekkanen",
                Email = "eramos@outlook.com",
                PhoneNumber = "658-978-245",
                DateOfBirth = DateTime.Parse("1997-10-09"),
                Address = "Random Alley 123",
                Gender = GenderOptions.Male,
                RegionId = region_from_add_2.RegionId,
                Nationality = "Finnish"
            };

            List<StudentAddRequest> studentAddRequests = new List<StudentAddRequest>(){student_request_1,student_request_2,
            student_request_3};

            List<StudentResponse> student_list_from_add = new List<StudentResponse>();

            foreach (StudentAddRequest? student in studentAddRequests)
            {
                StudentResponse studentResponse = _studentService.AddStudent(student);
                student_list_from_add.Add(studentResponse);
            }
            //Directive to print expected values
            _testOutputHelper.WriteLine("Expected: ");
            foreach (StudentResponse student_response_from_add in student_list_from_add)
            {
                //Printing values for a more detail test
                _testOutputHelper.WriteLine(student_response_from_add.ToString());
            }

            List<StudentResponse> student_list_from_search = _studentService.GetFilteredStudents(nameof(Student.StudentName), "ma");

            //Directive to print expected values
            _testOutputHelper.WriteLine("Actual: ");
            foreach (StudentResponse student_response_from_get in student_list_from_search)
            {
                _testOutputHelper.WriteLine(student_response_from_get.ToString());
            }

            foreach (StudentResponse student_response_from_add in student_list_from_add)
            {
                if(student_response_from_add.StudentName!=null)
                {
                    if(student_response_from_add.StudentName.Contains("ma",StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(student_response_from_add, student_list_from_search);

                    }
                }
            }

        }

        #endregion

        #region UpdateStudents()
        //When null value provided as parameter -> Argumentnullexception
        [Fact]
        public void UpdateStudents_NullId()
        {
            StudentUpdateRequest? student_request = null;

            Assert.Throws<ArgumentNullException>(()=>
            {
                _studentService.UpdateStudent(student_request);
            });
        }

        //When invalid ID provided -> Argument Exception
        [Fact]
        public void UpdateStudents_InvalidId()
        {
            StudentUpdateRequest? student_update = new StudentUpdateRequest() {StudentId=Guid.NewGuid()};

            Assert.Throws<ArgumentException>(()=>
            {
                _studentService.UpdateStudent(student_update);
            });
        }

        //When Student Name is null -> ArgumentException
        [Fact]
        public void UpdateStudents_NullStudentName()
        {
            RegionAddRequest? regionAddRequest = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionResponse regiomFromAdd = _regionsService.AddRegion(regionAddRequest);

            StudentAddRequest? studentAddRequest = new StudentAddRequest()
            {
                StudentName = "Jane Doe",
                RegionId =
            regiomFromAdd.RegionId
            };

            StudentResponse studentFromAdd = _studentService.AddStudent(studentAddRequest);

            StudentUpdateRequest studentUpdate= studentFromAdd.ToStudentUpdateRequest();

            studentUpdate.StudentName = null;

            Assert.Throws<ArgumentException>(()=>
            {
                _studentService.UpdateStudent(studentUpdate);
            });

        }

        //Proper  update of Student
        [Fact]
        public void UpdateStudents_ProperStudentUpdate()
        {


            RegionAddRequest? regionAddRequest = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionResponse regiomFromAdd = _regionsService.AddRegion(regionAddRequest);

            StudentAddRequest? studentAddRequest = new StudentAddRequest()
            {
                StudentName = "Jane Doe",
                RegionId =
            regiomFromAdd.RegionId,
                Email = "jd@example.com",
                PhoneNumber = "041-365-8795",
                DateOfBirth = DateTime.Parse("1999-01-25"),
                Address="Lincoln St. 125",
                Gender=GenderOptions.Female,
                Nationality="Ukranian"
            };

            StudentResponse studentFromAdd = _studentService.AddStudent(studentAddRequest);

            StudentUpdateRequest studentUpdate = studentFromAdd.ToStudentUpdateRequest();

            studentUpdate.StudentName = "Jennifer Doe";
            studentUpdate.Email = "jendoe@example.com";

            StudentResponse student_from_update = _studentService.UpdateStudent(studentUpdate);
            StudentResponse? student_from_get = _studentService.GetStudentByStudentId(student_from_update.StudentId);

            Assert.Equal(student_from_get,student_from_update);

        }

        #endregion

        #region DeleteStudent()
        [Fact]
        public void DeleteStudent_InvalidId()
        {
            bool isDeleted =_studentService.DeleteStudent(Guid.NewGuid());
            Assert.False(isDeleted);
        }
        [Fact]
        public void DeleteStudent_ValidId()
        {
            RegionAddRequest regionAddRequest = new RegionAddRequest() { RegionName="Österbotten"};
            RegionResponse regionFromAdd = _regionsService.AddRegion(regionAddRequest);

            StudentAddRequest studentAddRequest = new StudentAddRequest() {StudentName="Jane Doe",
            RegionId=regionFromAdd.RegionId, Email="janedoe@example.com", Address="123 Charming st.",
            PhoneNumber="046-356-8745",DateOfBirth=DateTime.Parse("1995-01-26"),Gender=GenderOptions.Female,
            Nationality="Brazilian"};

            StudentResponse studentFromAdd= _studentService.AddStudent(studentAddRequest);

            bool isDeleted = _studentService.DeleteStudent(studentFromAdd.StudentId);
            Assert.True(isDeleted);
        }
        #endregion
    }
}
