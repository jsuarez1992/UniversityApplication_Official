using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class RegionResponse
    {
        public Guid RegionId { get; set; }
        public string? RegionName { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj==null)
                return false;
            if(obj.GetType() != typeof(RegionResponse))
            { 
                return false;
            }

            RegionResponse region_to_compare = (RegionResponse)obj;

            return RegionId == region_to_compare.RegionId && RegionName == region_to_compare.RegionName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class RegionExtensions
    {
        public static RegionResponse ToRegionResponse(this Region region)
        {
            return new RegionResponse { RegionId = region.RegionId, RegionName = region.RegionName };
        }
    }

}
