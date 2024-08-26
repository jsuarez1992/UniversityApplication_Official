using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class UniversityFormResponse
    {
        public Guid UniversityFormId { get; set; }
        public string? UniversityFormName { get; set; }
        public string? SchoolName { get; set; }
        public DateTime EnrolledSince { get; set; }
        public DateTime EnrolledUntil { get; set; }
        public double? AverageScore { get; set; }
        public string? LevelOfStudy { get; set; }
        public Guid DegreeId { get; set; }
        public string? Degree {  get; set; }
        public string? SemesterEnrollment { get; set; }
        public string? FirstChoice { get; set; }
        public string? SecondChoice { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if (obj.GetType() != typeof(UniversityFormResponse)) return false;

            UniversityFormResponse universityForm = (UniversityFormResponse)obj;

            return UniversityFormId==universityForm.UniversityFormId && UniversityFormName==universityForm.
            UniversityFormName && SchoolName==universityForm.SchoolName && EnrolledSince==universityForm.
            EnrolledSince && EnrolledUntil==universityForm.EnrolledUntil && AverageScore == universityForm.
            AverageScore && LevelOfStudy == universityForm.LevelOfStudy && DegreeId == universityForm.DegreeId
            && SemesterEnrollment == universityForm.SemesterEnrollment && FirstChoice == universityForm.FirstChoice
            && SecondChoice==universityForm.SecondChoice;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Form Id={UniversityFormId}, Form Name ={UniversityFormName}, School Name={SchoolName}," +
                $"Enrolled Since={EnrolledSince.ToString("dd MMM yyyy")}, Enrolled Until=" +
                $"{EnrolledUntil.ToString("dd MMM yyyy")}, Average Score={AverageScore}, Level Of Study= " +
                $"{LevelOfStudy}, Degree Id ={DegreeId}, Degree Name = {Degree}, " +
                $"Semester Enrollment = {SemesterEnrollment}, First Choice={FirstChoice}, Second Choice={SecondChoice}";
        }
        public UniversityFormUpdateRequest ToUniversityFormUpdateRequest()
        {
            return new UniversityFormUpdateRequest() {UniversityFormId=UniversityFormId,UniversityFormName=
            UniversityFormName, SchoolName=SchoolName, EnrolledSince=EnrolledSince, EnrolledUntil=EnrolledUntil,
            LevelOfStudy = (LevelOfStudyOptions)Enum.Parse(typeof(LevelOfStudyOptions), LevelOfStudy, true),
            DegreeId=DegreeId, SemesterEnrollment = (SemesterEntry)Enum.Parse(typeof(SemesterEntry), 
            SemesterEnrollment, true), FirstChoice = FirstChoice, SecondChoice = SecondChoice,
            };
        }
    }
    public static class UniversityFormExtensions
    {
        public static UniversityFormResponse ToUniversityFormResponse(this UniversityForm universityForm)
        {
            return new UniversityFormResponse
            {
                UniversityFormId = universityForm.UniversityFormId,
                UniversityFormName = universityForm.UniversityFormName,
                SchoolName = universityForm.SchoolName,
                EnrolledSince = universityForm.EnrolledSince,
                EnrolledUntil = universityForm.EnrolledUntil,
                AverageScore = universityForm.AverageScore,
                SemesterEnrollment = universityForm.SemesterEnrollment,
                FirstChoice = universityForm.FirstChoice,
                SecondChoice = universityForm.SecondChoice
            };
        }
    }
}
