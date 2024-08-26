using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class UniversityFormUpdateRequest
    {
        [Required]
        public Guid UniversityFormId { get; set; }

        [Required(ErrorMessage = "Form Name cannot be blank")]
        public string? UniversityFormName { get; set; }
        public string? SchoolName { get; set; }
        public DateTime EnrolledSince { get; set; }
        public DateTime EnrolledUntil { get; set; }
        public float? AverageScore { get; set; }
        public LevelOfStudyOptions? LevelOfStudy { get; set; }
        public Guid DegreeId { get; set; }
        public SemesterEntry? SemesterEnrollment { get; set; }
        public string? FirstChoice { get; set; }
        public string? SecondChoice { get; set; }

        public UniversityForm ToUniversityForm()
        {
            return new UniversityForm()
            {
                UniversityFormId=UniversityFormId,
                UniversityFormName = UniversityFormName,
                SchoolName = SchoolName,
                EnrolledSince = EnrolledSince,
                EnrolledUntil = EnrolledUntil,
                AverageScore = AverageScore,
                LevelOfStudy = LevelOfStudy.ToString(),
                DegreeId = DegreeId,
                SemesterEnrollment = SemesterEnrollment.ToString(),
                FirstChoice = FirstChoice,
                SecondChoice = SecondChoice
            };
        }

    }
}
