using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RegionsService : IRegionsService
    {
        private readonly List<Region> _regions;
        public RegionsService()
        {
            _regions = new List<Region>();
        }
        public RegionResponse AddRegion(RegionAddRequest? regionAddRequest)
        {
            if (regionAddRequest == null)
            { 
                throw new ArgumentNullException(nameof(regionAddRequest));
            }
            if(regionAddRequest.RegionName==null)
            {
                throw new ArgumentException(nameof(regionAddRequest));
            }
            if(_regions.Where(temp => temp.RegionName == regionAddRequest.RegionName).Count()>0)
            {
                throw new ArgumentException("This region already exists.");
            }
            //If all is validated and correct, we proceed with this:
            Region region = regionAddRequest.ToRegion();

            region.RegionId=Guid.NewGuid();
            _regions.Add(region);

            return region.ToRegionResponse();
        }

        public List<RegionResponse> GetAllRegions()
        {
            return _regions.Select(region => region.ToRegionResponse()).ToList();
        }

        public RegionResponse? GetRegionByRegionId(Guid? regionId)
        {
            if (regionId == null)
                return null;

            Region? region_from_list = _regions.FirstOrDefault(temp => temp.RegionId == regionId);

            if(region_from_list == null)
                return null;

            return region_from_list.ToRegionResponse();
        }

        public bool DeleteRegion(Guid? regionId)
        {
            if (regionId == null)
                throw new ArgumentNullException(nameof(regionId));
            Region? region = _regions.FirstOrDefault(temp => temp.RegionId == regionId);
            if (region == null)
                return false;
            _regions.RemoveAll(temp=>temp.RegionId==regionId);

            return true;
        }

    }
}
