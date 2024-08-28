using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Validation;

namespace Services
{
    public class FacultiesService : IFacultiesService
    {
        private readonly List<Faculty> _faculties;
        public FacultiesService()
        {
            _faculties = new List<Faculty>();
        }
        public FacultyResponse AddFaculty(FacultyAddRequest? facultyAddRequest)
        {
            if(facultyAddRequest== null)
            {
                throw new ArgumentNullException(nameof(facultyAddRequest));
            }
            if(facultyAddRequest.DegreeName==null)
            {
                throw new ArgumentException(nameof(facultyAddRequest.DegreeName));
            }
            if(_faculties.Where(temp=>temp.DegreeName == facultyAddRequest.DegreeName).Count()>0)
            {
                throw new ArgumentException("Faculty is already registered");
            }
            //Add object properly
            Faculty faculty = facultyAddRequest.ToFaculty();

            faculty.DegreeId= Guid.NewGuid();
            _faculties.Add(faculty);

            return faculty.ToFacultyResponse();
        }

        public List<FacultyResponse> GetAllFaculties()
        {
            return _faculties.Select(faculty => faculty.ToFacultyResponse()).ToList();
        }

        public FacultyResponse? GetFacultyByFacultyId(Guid? degreeId)
        {
            if(degreeId== null)
                return null;

            Faculty? faculty_from_list = _faculties.FirstOrDefault(temp => temp.DegreeId == degreeId);

            if (faculty_from_list == null)
                return null;

            return faculty_from_list.ToFacultyResponse();
        }

        public FacultyResponse UpdateFaculty(FacultyUpdateRequest? facultyUpdateRequest)
        {
            //if facultyupdate requ is null -> argumentnullexc
            if (facultyUpdateRequest == null)
                throw new ArgumentNullException(nameof(Faculty));

            Faculty? matchingFaculty = _faculties.FirstOrDefault(temp=> temp.DegreeId ==facultyUpdateRequest.DegreeId);

            if (matchingFaculty == null)
                throw new ArgumentException("Given degree Id does not exist");

            // if DegreeName is null or empty -> throw ArgumentException
            if (string.IsNullOrEmpty(facultyUpdateRequest.DegreeName))
                throw new ArgumentException("DegreeName cannot be null or empty");

            matchingFaculty.DegreeName = facultyUpdateRequest.DegreeName;

            return matchingFaculty.ToFacultyResponse();
        }

        public bool DeleteFaculty(Guid? facultyId)
        {
            if(facultyId==null)
                throw new ArgumentNullException(nameof(facultyId));

            Faculty? faculty = _faculties.FirstOrDefault(temp=>temp.DegreeId==facultyId);
            if (faculty == null)
                return false;
            _faculties.RemoveAll(temp=>temp.DegreeId==facultyId);
            return true;
        }
    }
}
