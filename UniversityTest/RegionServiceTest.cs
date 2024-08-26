using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace UniversityTest
{
    public class RegionServiceTest
    {
        private readonly IRegionsService _regionsService;
        public RegionServiceTest()
        {
            _regionsService = new RegionsService();
        }
        #region AddRegion()

        [Fact]
        //Requirement 1: RegionAddRequest is null -> ArgumentNullException
        public void AddRegion_NullRegion()
        {
            //Arrange
            RegionAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _regionsService.AddRegion(request);
            });

        }

        //Requirement 2: RegionName is null -> ArgumentException
        [Fact]
        public void AddRegion_NullRegionName()
        {
            //Arrange
            RegionAddRequest? request = new RegionAddRequest() { RegionName = null };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _regionsService.AddRegion(request);
            });

        }
        //Requirement 3: RegionName is duplicate -> ArgumentException
        [Fact]
        public void AddRegion_DuplicateRegion()
        {
            //Arrange
            RegionAddRequest? request1 = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionAddRequest? request2 = new RegionAddRequest() { RegionName = "Österbotten" };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _regionsService.AddRegion(request1);
                _regionsService.AddRegion(request1);
            });            
        }
        //Requirement 4: proper name -> adding as usual
        [Fact]
        public void AddRegion_AddingRegion()
        {
            //Arrange
            RegionAddRequest? request = new RegionAddRequest() { RegionName="Österbotten"};
            //Assert
            RegionResponse response = _regionsService.AddRegion(request);
            List<RegionResponse> regions_from_GetAllRegions = _regionsService.GetAllRegions();
            //Assert
            Assert.True(response.RegionId != Guid.Empty);
            Assert.Contains(response,regions_from_GetAllRegions);
        }
        #endregion

        #region GetAllRegions()

        [Fact]
        //List should be empty before adding any regions
        public void GetAllRegions_EmptyList()
        {
            //Act
            List<RegionResponse> actual_region_list = _regionsService.GetAllRegions();
            //Assert
            Assert.Empty(actual_region_list);
        }

        [Fact]
        //If you add countries, they should be returned
        public void GetAllRegions_AddFewRegions()
        {
            //Arrange
            List<RegionAddRequest> region_request_list = new List<RegionAddRequest>() { 
                new RegionAddRequest(){RegionName="Österbotten"},
                new RegionAddRequest() {RegionName= "Iisalmi"},
            };
            //Act
            List<RegionResponse> regions_list_from_add = new List<RegionResponse>();

            foreach(RegionAddRequest region_request in region_request_list)
            {
                regions_list_from_add.Add(_regionsService.AddRegion(region_request));
            }
            List<RegionResponse> actual_response_list = _regionsService.GetAllRegions();

            foreach(RegionResponse expected_region in regions_list_from_add)
            {
                Assert.Contains(expected_region,actual_response_list);
            }

        }
        #endregion


        #region GetRegionByRegionId()
        //In case we get a null value
        [Fact]
        public void GetRegionByRegionId_RegionIdNull()
        {
            //Arrange
            Guid? regId = null;

            //Act
            RegionResponse? region_from_get = _regionsService.GetRegionByRegionId(regId);

            //Assert
            Assert.Null(region_from_get);
        }

        //Proper fetching if all validations are correct
        [Fact]
        public void GetRegionByRegionId_ValidId()
        {
            //Arrange
            RegionAddRequest? region_add_request = new RegionAddRequest() { RegionName = "Österbotten"};
            RegionResponse region_from_addrequest = _regionsService.AddRegion(region_add_request);

            //Act
            RegionResponse? region_from_get = _regionsService.GetRegionByRegionId(region_from_addrequest.RegionId);

            //Assert
            Assert.Equal(region_from_addrequest,region_from_get);
        }


        #endregion

        #region DeleteRegion()
        [Fact]
        public void DeleteRegion_InvalidId()
        {
            bool isDeleted = _regionsService.DeleteRegion(Guid.NewGuid());
            Assert.False(isDeleted);
        }

        [Fact]
        public void DeleteRegion_ValidId()
        {
            RegionAddRequest regionAddRequest = new RegionAddRequest() { RegionName = "Österbotten" };
            RegionResponse regionFromAdd = _regionsService.AddRegion(regionAddRequest);

            bool isDeleted = _regionsService.DeleteRegion(regionFromAdd.RegionId);
            Assert.True(isDeleted);
        }
        #endregion
    }
}