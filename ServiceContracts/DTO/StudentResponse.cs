using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StudentResponse
    {
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? Age { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public Guid RegionId { get; set; }
        public string? Region { get; set; }
        public string? Nationality { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(StudentResponse)) return false;

            StudentResponse student= (StudentResponse)obj;
            return StudentId==student.StudentId && StudentName==student.StudentName && Email==student.Email &&
            PhoneNumber==student.PhoneNumber && DateOfBirth==student.DateOfBirth && Age==student.Age &&
            Address==student.Address && Gender==student.Gender && RegionId==student.RegionId && Region==
            student.Region && Nationality==student.Nationality;


        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override string ToString()
        {
            return $"Student Id={StudentId}, Student Name={StudentName},Email ={Email}, Phone Number={PhoneNumber}," +
                $"Date Of Birth={DateOfBirth}, Age={Age}, Address={Address}, Gender={Gender}, Region Id={RegionId}" +
                $"Region Name={Region}, Nationality={Nationality}";
        }

        public StudentUpdateRequest ToStudentUpdateRequest()
        {
            return new StudentUpdateRequest() { StudentId=StudentId,StudentName=StudentName,Email=Email,PhoneNumber=
            PhoneNumber,DateOfBirth=DateOfBirth,Address=Address, Gender = (GenderOptions)Enum.Parse(typeof(
            GenderOptions), Gender, true), RegionId=RegionId,Nationality = Nationality
            };
        }
    }

    public static class StudentExtensions
    {
        public static StudentResponse ToStudentResponse(this Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth,
                Age = (student.DateOfBirth != null) ? Math.Round((DateTime.Now - student.DateOfBirth.Value).
                TotalDays / 365.25) : null,
                Address = student.Address,
                Gender = student.Gender,
                RegionId = student.RegionId,
                Nationality = student.Nationality
            };
        }
    }
}
