using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class FacultyAddRequest
    {
        public string? DegreeName { get; set; }
        public Faculty ToFaculty()
        {
            return new Faculty() { DegreeName = DegreeName };
        }
    }
}
