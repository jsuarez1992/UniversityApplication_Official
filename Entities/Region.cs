using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Domain model for Region
    /// </summary>
    public class Region
    {
        [Key]
        public Guid RegionId {  get; set; }
        public string? RegionName {  get; set; }
    }
}
