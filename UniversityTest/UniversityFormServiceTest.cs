using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UniversityTest
{
    public class UniversityFormServiceTest 
    {
        private readonly IUniversityFormsService _universityFormService;
        private readonly IFacultiesService _facultiesService;
        private readonly ITestOutputHelper _testOutputHelper;
        public UniversityFormServiceTest(ITestOutputHelper testOutputHelper)
        {
            _universityFormService= new UniversityFormsService();
            _facultiesService= new FacultiesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddUniversityForm()
        //when adding a null object -> null in return
        [Fact]
        public void AddUniversityForm_NullForm()
        {
            UniversityFormAddRequest? form_request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _universityFormService.AddUniversityForm(form_request);
            });
        }

        //when null value gets supplied as formname -> ArgumentException
        [Fact]
        public void AddUniversityForm_NullFormName()
        {
            UniversityFormAddRequest form_request_name = new UniversityFormAddRequest() { UniversityFormName = 
            null};

            Assert.Throws<ArgumentException>(() =>
            {
                _universityFormService.AddUniversityForm(form_request_name);
            });
        }

        //when all values are added -> proper adding
        [Fact]
        public void AddUniversityForm_ProperFormAdding()
        {
            UniversityFormAddRequest? form_request_add = new UniversityFormAddRequest() { UniversityFormName=
            "EntranceForm", SchoolName="Karleby High School", EnrolledSince=DateTime.Parse("2019-03-25"),
            EnrolledUntil=DateTime.Parse("2024-01-24"),AverageScore = (float)4.00, LevelOfStudy=LevelOfStudyOptions.
            Undergraduate, DegreeId=Guid.NewGuid(),SemesterEnrollment=SemesterEntry.Fall, FirstChoice="Biology", 
            SecondChoice="Economy"};

            UniversityFormResponse university_form_from_addreq = _universityFormService.AddUniversityForm(form_request_add);

            List<UniversityFormResponse> university_form_list = _universityFormService.GetAllUniversityForm();

            Assert.True(university_form_from_addreq.UniversityFormId != Guid.Empty);
            Assert.Contains(university_form_from_addreq,university_form_list);

        }
        #endregion

        #region GetFormByFormId()
        //Requirement 1: If we supply a null value as a Person ID, the PersonResponse? object should also be null.
        [Fact]
        public void GetFormByFormId_FormIdIsNull()
        {
            Guid? formId = null;

            UniversityFormResponse? form_from_get = _universityFormService.GetFormByFormId(formId);
            Assert.Null(form_from_get);
        }

        //Requirement 2: If we supply a valid person ID, then it should return the proper value as a PersonResponse? object.
        [Fact]
        public void GetFormByFormId_ProperIdReturn()
        {
            FacultyAddRequest faculty_request= new FacultyAddRequest() { DegreeName="Biochemistry"};
            FacultyResponse faculty_response = _facultiesService.AddFaculty(faculty_request);

            UniversityFormAddRequest university_form_request = new UniversityFormAddRequest() {UniversityFormName="Admission_PabloEscobar",
            SchoolName="Karleby High School", EnrolledSince=DateTime.Parse("2020-06-30"), EnrolledUntil=DateTime.Parse("2024-08-15"),
            AverageScore=(float)4.20, LevelOfStudy=LevelOfStudyOptions.Undergraduate,DegreeId=faculty_response.DegreeId,
            SemesterEnrollment=SemesterEntry.Fall,FirstChoice="BioChemistry",SecondChoice="Mechanics"};

            UniversityFormResponse university_form_from_add =_universityFormService.AddUniversityForm(university_form_request);
            UniversityFormResponse? university_form_from_get = _universityFormService.GetFormByFormId(university_form_from_add.UniversityFormId);
            Assert.Equal(university_form_from_add,university_form_from_get);
        }

        #endregion

        #region GetAllForms()
        //The GetAllPersons() method should return an empty list by default
        [Fact]
        public void GetAllForms_EmptyList()
        {
            List<UniversityFormResponse> university_form_get = _universityFormService.GetAllUniversityForm();
            Assert.Empty(university_form_get);
        }

        //After adding a few persons and then calling GetAllPersons(),
        //it should return the same persons that we added before.
        [Fact]
        public void GetAllForm_ProperAddReturn()
        {
            FacultyAddRequest faculty_request_1 = new FacultyAddRequest() { DegreeName="Information Technology"};
            FacultyAddRequest faculty_request_2 = new FacultyAddRequest() { DegreeName = "Biochemistry" };
            FacultyResponse faculty_response_1= _facultiesService.AddFaculty(faculty_request_1);
            FacultyResponse faculty_response_2 = _facultiesService.AddFaculty(faculty_request_2);

            UniversityFormAddRequest university_form_addreq_1 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_PedroPerez",
                SchoolName = "Karleby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.00,
                LevelOfStudy = LevelOfStudyOptions.
            Undergraduate,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Fall,
                FirstChoice = "Biology",
                SecondChoice = "Economy"
            };

            UniversityFormAddRequest university_form_addreq_2 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_MariaMorales",
                SchoolName = "Kronoby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.25,
                LevelOfStudy = LevelOfStudyOptions.
            Undergraduate,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Fall,
                FirstChoice = "Mechanics",
                SecondChoice = "Business"
            };
            UniversityFormAddRequest university_form_addreq_3 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_MAnttuPekannen",
                SchoolName = "Kronoby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.18,
                LevelOfStudy = LevelOfStudyOptions.
            Master,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Spring,
                FirstChoice = "Robotics",
                SecondChoice = "Artificial Intelligence"
            };
            List<UniversityFormAddRequest> formsAddRequest = new List<UniversityFormAddRequest>() {university_form_addreq_1,
            university_form_addreq_2,university_form_addreq_3};
            List<UniversityFormResponse> forms_from_add = new List<UniversityFormResponse>();


            foreach (UniversityFormAddRequest? uniforms in formsAddRequest)
            {
                UniversityFormResponse universityFormResponse = _universityFormService.AddUniversityForm(uniforms);
                forms_from_add.Add(universityFormResponse);

            }
            //Directive to print expected values
            _testOutputHelper.WriteLine("Expected: ");

            foreach(UniversityFormResponse uniforms_from_addreq in forms_from_add)
            {
                //Appending properties/values from list with TestOutputHelper
                _testOutputHelper.WriteLine(uniforms_from_addreq.ToString());
            };

            List<UniversityFormResponse> uniforms_from_get = _universityFormService.GetAllUniversityForm();
            //Directive to print actual values:
            _testOutputHelper.WriteLine("Actual: ");

            foreach(UniversityFormResponse uniforms_response_from_get in uniforms_from_get)
            {
                //Appending properties/values from list with TestOutputHelper
                _testOutputHelper.WriteLine(uniforms_response_from_get.ToString());
            }

            foreach (UniversityFormResponse uniforms_from_add in forms_from_add)
            {
                Assert.Contains(uniforms_from_add,uniforms_from_get);
            }

        }


        #endregion

        #region GetSortedForms()
        [Fact]
        public void GetSortedForms()
        {
            FacultyAddRequest faculty_request_1 = new FacultyAddRequest() { DegreeName = "Information Technology" };
            FacultyAddRequest faculty_request_2 = new FacultyAddRequest() { DegreeName = "Biochemistry" };
            FacultyResponse faculty_response_1 = _facultiesService.AddFaculty(faculty_request_1);
            FacultyResponse faculty_response_2 = _facultiesService.AddFaculty(faculty_request_2);

            UniversityFormAddRequest university_form_addreq_1 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_PedroPerez",
                SchoolName = "Karleby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.00,
                LevelOfStudy = LevelOfStudyOptions.
            Undergraduate,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Fall,
                FirstChoice = "Biology",
                SecondChoice = "Economy"
            };

            UniversityFormAddRequest university_form_addreq_2 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_MariaMorales",
                SchoolName = "Kronoby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.25,
                LevelOfStudy = LevelOfStudyOptions.
            Undergraduate,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Fall,
                FirstChoice = "Mechanics",
                SecondChoice = "Business"
            };
            UniversityFormAddRequest university_form_addreq_3 = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "EntranceForm_MAnttuPekannen",
                SchoolName = "Kronoby High School",
                EnrolledSince = DateTime.Parse("2019-03-25"),
                EnrolledUntil = DateTime.Parse("2024-01-24"),
                AverageScore = (float)4.18,
                LevelOfStudy = LevelOfStudyOptions.
            Master,
                DegreeId = faculty_response_1.DegreeId,
                SemesterEnrollment = SemesterEntry.Spring,
                FirstChoice = "Robotics",
                SecondChoice = "Artificial Intelligence"
            };


            List<UniversityFormAddRequest> formsAddRequest = new List<UniversityFormAddRequest>() {university_form_addreq_1,
            university_form_addreq_2,university_form_addreq_3};
            List<UniversityFormResponse> forms_from_add = new List<UniversityFormResponse>();


            foreach (UniversityFormAddRequest? uniforms in formsAddRequest)
            {
                UniversityFormResponse universityFormResponse = _universityFormService.AddUniversityForm(uniforms);
                forms_from_add.Add(universityFormResponse);
            }


            //Directive to print expected values
            _testOutputHelper.WriteLine("Expected: ");

            foreach (UniversityFormResponse uniforms_from_addreq in forms_from_add)
            {
                //Appending properties/values from list with TestOutputHelper
                _testOutputHelper.WriteLine(uniforms_from_addreq.ToString());
            };

            List<UniversityFormResponse> allForms=_universityFormService.GetAllUniversityForm();

            List<UniversityFormResponse> uniforms_from_sort = _universityFormService.GetSortedForms(allForms,nameof(
            UniversityForm.UniversityFormName),SortOrderOptions.DESC);


            //Directive to print actual values:
            _testOutputHelper.WriteLine("Actual: ");

            foreach (UniversityFormResponse uniforms_response_from_get in uniforms_from_sort)
            {
                //Appending properties/values from list with TestOutputHelper
                _testOutputHelper.WriteLine(uniforms_response_from_get.ToString());
            }

            forms_from_add = forms_from_add.OrderByDescending(temp=>temp.UniversityFormName).ToList();

            for (int i=0; i < forms_from_add.Count; i++)
            {
                Assert.Equal(forms_from_add[i], uniforms_from_sort[i]);
            }

        }
        #endregion

        #region UpdateForms()
        //when parameter is null -> ArgumentNullException
        [Fact]
        public void UpdateForms_NullId()
        {
            UniversityFormUpdateRequest? university_form = null;

            Assert.Throws<ArgumentNullException>(()=>
            {
                _universityFormService.UpdateForms(university_form);
            });
        }

        //When invalid Id provided -> Argumentxception
        [Fact]
        public void UpdateForms_InvalidId()
        {
            UniversityFormUpdateRequest? form_request = new UniversityFormUpdateRequest() {UniversityFormId=Guid.
            NewGuid()};

            Assert.Throws<ArgumentException>(()=>
            {
                _universityFormService.UpdateForms(form_request);
            });
        }

        //When null form name provided -> ArgumentException
        [Fact]
        public void UpdateForms_NullFormName()
        {
            FacultyAddRequest facultyFromAdd = new FacultyAddRequest() { DegreeName = "Biology" };
            FacultyResponse facultyResponse = _facultiesService.AddFaculty(facultyFromAdd);

            UniversityFormAddRequest formAddRequest = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "ApplicationForm_JaneDoe",
                DegreeId = facultyResponse.DegreeId,
                LevelOfStudy=LevelOfStudyOptions.Undergraduate,
                SemesterEnrollment=SemesterEntry.Spring,
            };

            UniversityFormResponse formResponse = _universityFormService.AddUniversityForm(formAddRequest);

            UniversityFormUpdateRequest formUpdateRequest = formResponse.ToUniversityFormUpdateRequest();

            formUpdateRequest.UniversityFormName = null;

            Assert.Throws<ArgumentException>(() =>
            {
                _universityFormService.UpdateForms(formUpdateRequest);
            });
        }

        //Proper updating of universityFormTest        
        [Fact]
        public void UpdateForms_ProperFormUpdate()
        {
            FacultyAddRequest? facultyFromAdd = new FacultyAddRequest() { DegreeName = "Biology" };
            FacultyResponse facultyResponse = _facultiesService.AddFaculty(facultyFromAdd);

            UniversityFormAddRequest? formAddRequest = new UniversityFormAddRequest()
            {
                UniversityFormName =
            "ApplicationForm_JaneDoe",
                DegreeId = facultyResponse.DegreeId,
                SchoolName = "Karleby High Scool",
                EnrolledSince = DateTime.Parse("2019-01-12"),
                EnrolledUntil = DateTime.Parse("2024-06-12"),
                AverageScore=(float)4.12,
                LevelOfStudy= LevelOfStudyOptions.Undergraduate,
                SemesterEnrollment=SemesterEntry.Fall,
                FirstChoice="Biochemistry",
                SecondChoice="Economy",
            };

            UniversityFormResponse formResponse = _universityFormService.AddUniversityForm(formAddRequest);

            UniversityFormUpdateRequest formUpdateRequest = formResponse.ToUniversityFormUpdateRequest();

            formUpdateRequest.UniversityFormName = "ApplicationForm_JaneDoe";
            formUpdateRequest.SchoolName = "Pietarsaari High School";

            UniversityFormResponse form_from_update =_universityFormService.UpdateForms(formUpdateRequest);
            UniversityFormResponse? form_from_get = _universityFormService.GetFormByFormId(form_from_update.UniversityFormId);

            Assert.Equal(form_from_get,form_from_update);
        }


        #endregion

        #region DeleteForm()
        [Fact]
        public void DeleteForm_InvalidId()
        {
            bool isDeleted = _universityFormService.DeleteForm(Guid.NewGuid());
            Assert.False(isDeleted);
        }

        [Fact]
        public void DeleteForm_ValidId()
        {
            FacultyAddRequest facultyAddRequest=new FacultyAddRequest() { DegreeName="Archeology"};
            FacultyResponse faculty_from_add= _facultiesService.AddFaculty(facultyAddRequest);

            UniversityFormAddRequest formAddRequest = new UniversityFormAddRequest() {UniversityFormName=
            "ApplicationForm_JaneDoe",DegreeId=faculty_from_add.DegreeId, SchoolName="ABC School", EnrolledSince=
            DateTime.Parse("2020-07-27"), EnrolledUntil=DateTime.Parse("2023-12-24"), AverageScore=(float)4.15,
            LevelOfStudy=LevelOfStudyOptions.Undergraduate,SemesterEnrollment=SemesterEntry.Spring,FirstChoice="Mechanics",
            SecondChoice="History"};
            

            UniversityFormResponse universityFormResponse = _universityFormService.AddUniversityForm(formAddRequest);
            bool isDeleted = _universityFormService.DeleteForm(universityFormResponse.UniversityFormId);

            Assert.True(isDeleted);

        }
        #endregion
    }
}
