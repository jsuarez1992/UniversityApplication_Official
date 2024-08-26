using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class FacultyResponse
    {
        public Guid DegreeId { get; set; }
        public string? DegreeName { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
                return false;
            if(obj.GetType() != typeof(FacultyResponse))
                return false;

            FacultyResponse faculty_to_compare = (FacultyResponse)obj;

            return DegreeId == faculty_to_compare.DegreeId && DegreeName == faculty_to_compare.DegreeName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public FacultyUpdateRequest ToFacultyUpdateRequest()
        {
            return new FacultyUpdateRequest() {DegreeId=DegreeId,DegreeName=DegreeName};
        }
    }
    public static class FacultyExtensions
    {
        public static FacultyResponse ToFacultyResponse(this Faculty faculty)
        {
            return new FacultyResponse { DegreeId =faculty.DegreeId, DegreeName = faculty.DegreeName };
        }
    }
}

