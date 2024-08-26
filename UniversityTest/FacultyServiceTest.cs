using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityTest
{
    public class FacultyServiceTest
    {
        private readonly IFacultiesService _facultiesService;
        public FacultyServiceTest()
        {
            _facultiesService = new FacultiesService();
        }
        #region AddFaculty
        [Fact]
        //Added null faculty -> ArgumentNullException
        public void AddFaculty_NullFaculty()
        {
            //Arrange
            FacultyAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _facultiesService.AddFaculty(request);
            });
            //Add
        }
        //Added faculty obj(null FacultyName) -> ArgumentException
        [Fact]
        public void AddFaculty_NullFacultyName()
        {
            //Arrange
            FacultyAddRequest? request = new FacultyAddRequest() { DegreeName = null };
            //Assert
            Assert.Throws<ArgumentException>(() => {
                _facultiesService.AddFaculty(request);
            });
            //Act
        }
        //Added duplicate obj -> ArgumentException
        [Fact]
        public void AddFaculty_DuplicateFaculty()
        {
            //Arrange
            FacultyAddRequest? request1 = new FacultyAddRequest() { DegreeName = "Business" };
            FacultyAddRequest? request2 = new FacultyAddRequest() { DegreeName = "Business" };
            //Assert
            Assert.Throws<ArgumentException>(() => {
                _facultiesService.AddFaculty(request1);
                _facultiesService.AddFaculty(request1);
            });
        }
        //Proper faculty adding
        [Fact]
        public void AddFaculty_AddingFaculty()
        {
            //Arrange
            FacultyAddRequest? request = new FacultyAddRequest() {DegreeName="Finance"};
            //Act
            FacultyResponse response = _facultiesService.AddFaculty(request);
            List<FacultyResponse> faculties_from_GetAllFaculties = _facultiesService.GetAllFaculties();
            //Assert
            Assert.True(response.DegreeId != Guid.Empty);
            Assert.Contains(response,faculties_from_GetAllFaculties);
        }
        #endregion

        #region GetAllFaculties()
        //List should be empty by default
        [Fact]
        public void GetAllFaculties_EmptyList()
        {
            List<FacultyResponse> actual_region_list = _facultiesService.GetAllFaculties();

            //Assert
            Assert.Empty(actual_region_list);
        }

        //If you add few faculties, they have to be returned
        [Fact]
        public void GetAllFaculties_AddFewFaculties()
        {
            //Assert
            List<FacultyAddRequest> faculty_request_list = new List<FacultyAddRequest>() { 
                new FacultyAddRequest() { DegreeName = "Finance"},
                new FacultyAddRequest() { DegreeName = "Business"},
            };

            //Act
            List<FacultyResponse> faculty_list_from_add = new List<FacultyResponse>();

            foreach(FacultyAddRequest faculty_request in faculty_request_list)
            {
                faculty_list_from_add.Add(_facultiesService.AddFaculty(faculty_request));
            }
            List<FacultyResponse> actual_response_list = _facultiesService.GetAllFaculties();

            foreach(FacultyResponse expected_faculty in faculty_list_from_add)
            {
                Assert.Contains(expected_faculty,actual_response_list);
            }
        }


        #endregion

        #region GetFacultyByFacultyId()
        //If Faculty Id is null -> returns null object
        [Fact]
        public void GetFacultyByFacultyId_FacultyIdNull()
        {
            //Arrange
            Guid? facultyId = null;
            
            //Arrange
            FacultyResponse? faculty_from_get = _facultiesService.GetFacultyByFacultyId(facultyId);

            //Assert
            Assert.Null(faculty_from_get);
            
        }

        //If faculty Id valid -> adds the faculty properly using our add method
        [Fact]
        public void GetFacultyByFacultyId_ValidId()
        {
            //Arrange
            FacultyAddRequest faculty_add = new FacultyAddRequest() { DegreeName="Biochemistry"};
            FacultyResponse faculty_from_addrequest = _facultiesService.AddFaculty(faculty_add);

            //Act
            FacultyResponse? faculty_from_get = _facultiesService.GetFacultyByFacultyId(faculty_from_addrequest.DegreeId);

            //Assert
            Assert.Equal(faculty_from_addrequest,faculty_from_get);
        }

        #endregion

        #region UpdateFaculty()
        //Null value -> throws ArgumentNullExc
        [Fact]
        public void UpdateFaculty_NullId()
        {
            FacultyUpdateRequest? facultyUpdateRequest = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                _facultiesService.UpdateFaculty(facultyUpdateRequest);
            });
        }

        //Invalid ID supplied -> ArgumentException
        [Fact]
        public void UpdateFaculty_InvalidId()
        {
            FacultyUpdateRequest? facultyUpdateRequest = new FacultyUpdateRequest() { DegreeId = Guid.NewGuid() };
            Assert.Throws<ArgumentException>(() =>
            {
                _facultiesService.UpdateFaculty(facultyUpdateRequest);
            });

        }

        //when Person Name is null -> ArgumentException
        [Fact]
        public void UpdateFaculty_NullName()
        {
            FacultyAddRequest? facultyAddRequest = new FacultyAddRequest() {DegreeName="Economy"};
            FacultyResponse? facultyResponse = _facultiesService.AddFaculty(facultyAddRequest);

            FacultyUpdateRequest faculty_Update= facultyResponse.ToFacultyUpdateRequest();
            faculty_Update.DegreeName = null;

            Assert.Throws<ArgumentException>(()=>
            {
                _facultiesService.UpdateFaculty(faculty_Update);
            });
        }

        //Proper update of FacultyService
        [Fact]
        public void UpdateFaculty_ProperUpdating()
        {
            FacultyAddRequest? facultyAddRequest = new FacultyAddRequest() { DegreeName = "Economy" };
            FacultyResponse? facultyResponse = _facultiesService.AddFaculty(facultyAddRequest);

            FacultyUpdateRequest faculty_Update = facultyResponse.ToFacultyUpdateRequest();
            faculty_Update.DegreeName = "Business";

            FacultyResponse faculty_from_update = _facultiesService.UpdateFaculty(faculty_Update);
            FacultyResponse? faculty_from_get = _facultiesService.GetFacultyByFacultyId(faculty_from_update.DegreeId);

            Assert.Equal(faculty_from_get,faculty_from_update);
        }
        #endregion

        #region DeleteFaculty()

        [Fact]
        public void DeleteFaculty_InvalidId()
        {
            bool isDeleted = _facultiesService.DeleteFaculty(Guid.NewGuid());
            Assert.False(isDeleted);
        }
        [Fact]
        public void DeleteFaculty_ValidId()
        {
            FacultyAddRequest facultyAddRequest = new FacultyAddRequest() { DegreeName = "Cartography" };
            FacultyResponse facultyFromAdd = _facultiesService.AddFaculty(facultyAddRequest);

            bool isDeleted = _facultiesService.DeleteFaculty(facultyFromAdd.DegreeId);
            Assert.True(isDeleted);
        }
        #endregion
    }

}
