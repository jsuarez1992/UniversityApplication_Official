using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UniversityFormsService : IUniversityFormsService
    {
        private readonly List<UniversityForm> _forms;
        private readonly IFacultiesService _facultiesService;
        public UniversityFormsService()
        {
            _forms = new List<UniversityForm>();
            _facultiesService = new FacultiesService();
        }
        //Helper method that converts UniversityForm response and loads the degree name at the same time
        private UniversityFormResponse ConvertUniversityFormToUniversityFormResponse(UniversityForm universityForm)
        {
            UniversityFormResponse universityFormResponse = universityForm.ToUniversityFormResponse();
            universityFormResponse.Degree = _facultiesService.GetFacultyByFacultyId(universityForm.DegreeId)?.DegreeName;
            return universityFormResponse;
        }
        public UniversityFormResponse AddUniversityForm(UniversityFormAddRequest? universityFormAddRequest)
        {

            if (universityFormAddRequest == null)
                throw new ArgumentNullException(nameof(StudentAddRequest));

            ValidationHelper_UniversityForm.ModelValidation(universityFormAddRequest);


            UniversityForm universityForm = universityFormAddRequest.ToUniversityForm();
            universityForm.UniversityFormId=Guid.NewGuid();
            _forms.Add(universityForm);
            return ConvertUniversityFormToUniversityFormResponse(universityForm);
        }

        public List<UniversityFormResponse> GetAllUniversityForm()
        {
            return _forms.Select(temp=>temp.ToUniversityFormResponse()).ToList();
        }

        public UniversityFormResponse? GetFormByFormId(Guid? universityFormId)
        {
        
            if (universityFormId == null)
                return null;

            UniversityForm? universityForm = _forms.FirstOrDefault(temp => temp.UniversityFormId==universityFormId);
            if (universityForm == null)
                return null;

            return universityForm.ToUniversityFormResponse();
        }

        public List<UniversityFormResponse> GetSortedForms(List<UniversityFormResponse> allForms, string sortBy, SortOrderOptions sortOrderOptions)
        {
            if(string.IsNullOrEmpty(sortBy)) 
                return allForms;

            //If not null, create a list of UFormsresponse to store SortBy and Sort order, depending on the values,
            //return Order By ASC or DESC

            List<UniversityFormResponse> sortedForms = (sortBy, sortOrderOptions) switch
            {
                (nameof(UniversityFormResponse.UniversityFormName), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.
                UniversityFormName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.UniversityFormName), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.
                UniversityFormName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(UniversityFormResponse.SchoolName), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.SchoolName,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.SchoolName), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.SchoolName,
                StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(UniversityFormResponse.EnrolledSince), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.EnrolledSince
                ).ToList(),
                (nameof(UniversityFormResponse.EnrolledSince), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.EnrolledSince
                ).ToList(),

                (nameof(UniversityFormResponse.EnrolledUntil), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.EnrolledUntil
                ).ToList(),
                (nameof(UniversityFormResponse.EnrolledUntil), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.EnrolledUntil
                ).ToList(),

                (nameof(UniversityFormResponse.AverageScore), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.AverageScore)
                .ToList(),
                (nameof(UniversityFormResponse.AverageScore), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.AverageScore)
                .ToList(),

                (nameof(UniversityFormResponse.LevelOfStudy), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.LevelOfStudy,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.LevelOfStudy), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.LevelOfStudy,
                StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(UniversityFormResponse.Degree), SortOrderOptions.ASC) => allForms.OrderBy(temp => temp.Degree,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.Degree), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.Degree,
                StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(UniversityFormResponse.SemesterEnrollment),SortOrderOptions.ASC)=>allForms.OrderBy(temp => temp.SemesterEnrollment,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.SemesterEnrollment), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.SemesterEnrollment,
                StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(UniversityFormResponse.FirstChoice),SortOrderOptions.ASC)=>allForms.OrderBy(temp => temp.FirstChoice,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.FirstChoice), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.FirstChoice,
                StringComparer.OrdinalIgnoreCase).ToList(),


                (nameof(UniversityFormResponse.SecondChoice),SortOrderOptions.ASC)=>allForms.OrderBy(temp => temp.SecondChoice,
                StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UniversityFormResponse.SecondChoice), SortOrderOptions.DESC) => allForms.OrderByDescending(temp => temp.SecondChoice,
                StringComparer.OrdinalIgnoreCase).ToList(),


                _ => allForms

            };
            return sortedForms;
        }

        public UniversityFormResponse UpdateForms(UniversityFormUpdateRequest universityFormUpdateRequest)
        {
            //If uniformupdatereq is null -> argumentnullexception
            if (universityFormUpdateRequest == null)
                throw new ArgumentNullException(nameof(UniversityForm));

            ValidationHelper_UniversityForm.ModelValidation(universityFormUpdateRequest);

            UniversityForm? matchingForm = _forms.FirstOrDefault(temp=>temp.UniversityFormId==
            universityFormUpdateRequest.UniversityFormId);

            if (matchingForm == null)
                throw new ArgumentException("Given Form Id does not exist");

            matchingForm.UniversityFormName= universityFormUpdateRequest.UniversityFormName;
            matchingForm.SchoolName=universityFormUpdateRequest.SchoolName;
            matchingForm.EnrolledSince=universityFormUpdateRequest.EnrolledSince;
            matchingForm.EnrolledUntil=universityFormUpdateRequest.EnrolledUntil;
            matchingForm.AverageScore=universityFormUpdateRequest.AverageScore;
            matchingForm.LevelOfStudy = universityFormUpdateRequest.LevelOfStudy.ToString();
            matchingForm.DegreeId = universityFormUpdateRequest.DegreeId;
            matchingForm.SemesterEnrollment = universityFormUpdateRequest.SemesterEnrollment.ToString();
            matchingForm.FirstChoice=universityFormUpdateRequest.FirstChoice;
            matchingForm.SecondChoice = universityFormUpdateRequest.SecondChoice;

            return matchingForm.ToUniversityFormResponse();
        }

        public bool DeleteForm(Guid? formId)
        {
            if (formId == null)
                throw new ArgumentNullException(nameof(formId));

            UniversityForm? universityForm = _forms.FirstOrDefault(temp=>temp.UniversityFormId== formId);

            if (universityForm == null)
                return false;
            _forms.RemoveAll(temp=>temp.UniversityFormId==formId);
            return true;
        }
    }
}
