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
    public class StudentUpdateRequest
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Form Name cannot be blank")]
        public string? StudentName { get; set; }
        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email should be in valid format")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid RegionId { get; set; }
        public string? Nationality { get; set; }

        public Student ToStudent()
        {
            return new Student()
            {
                StudentId = StudentId,
                StudentName = StudentName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                DateOfBirth = DateOfBirth,
                Address = Address,
                Gender = Gender.ToString(),
                RegionId = RegionId,
                Nationality = Nationality
            };
        }
    }
}
