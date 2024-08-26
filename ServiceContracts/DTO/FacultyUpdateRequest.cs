using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class FacultyUpdateRequest
    {
        [Required]
        public Guid DegreeId { get; set; }
        public string? DegreeName { get; set; }

        public Faculty ToFaculty()
        { 
            return new Faculty { DegreeId=DegreeId, DegreeName = DegreeName }; 
        }
    }
}
