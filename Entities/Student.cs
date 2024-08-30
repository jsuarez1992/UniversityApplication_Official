using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        public string? StudentName {  get; set; }
        public string? Email {  get; set; }
        public string? PhoneNumber {  get; set; }
        public DateTime? DateOfBirth {  get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public Guid RegionId { get; set; }
        public string? Nationality { get; set; }
    }
}
