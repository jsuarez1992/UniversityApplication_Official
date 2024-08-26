using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IRegionsService
    {
        RegionResponse AddRegion(RegionAddRequest? regionAddRequest);
        List<RegionResponse> GetAllRegions();
        RegionResponse?GetRegionByRegionId(Guid? regionId);
        bool DeleteRegion(Guid? regionId);
    }
}
