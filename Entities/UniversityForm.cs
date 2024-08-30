using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UniversityForm
    {
        [Key]
        public Guid UniversityFormId { get; set; }
        public string? UniversityFormName { get; set; }
        public string? SchoolName { get; set; }
        public DateTime EnrolledSince {  get; set; }
        public DateTime EnrolledUntil { get; set; }
        public double? AverageScore { get; set; }
        public string? LevelOfStudy { get; set; }
        public Guid DegreeId { get; set; }
        public string? SemesterEnrollment { get; set; }
        public string? FirstChoice { get; set; }
        public string? SecondChoice { get; set; }
    }
}
