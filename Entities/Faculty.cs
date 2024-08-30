using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Faculty
    {
        [Key]
        public Guid DegreeId { get; set; }
        public string? DegreeName { get; set; }
    }
}
