using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding new Region
    /// </summary>
    public class RegionAddRequest
    {
        public string? RegionName { get; set; }
        public Region ToRegion()
        {
            return new Region() { RegionName = RegionName };
        }
    }

}
