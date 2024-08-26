using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IFacultiesService
    {
        FacultyResponse AddFaculty(FacultyAddRequest? facultyAddRequest);
        List<FacultyResponse> GetAllFaculties();
        FacultyResponse? GetFacultyByFacultyId(Guid? degreeId);
        FacultyResponse UpdateFaculty(FacultyUpdateRequest? facultyUpdateRequest);
        bool DeleteFaculty(Guid? facultyId);
    }
}
