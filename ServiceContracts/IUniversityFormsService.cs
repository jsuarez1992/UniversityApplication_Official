using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IUniversityFormsService
    {
        UniversityFormResponse AddUniversityForm(UniversityFormAddRequest? universityFormAddRequest);
        List<UniversityFormResponse> GetAllUniversityForm();
        UniversityFormResponse? GetFormByFormId(Guid? universityFormId);
        List<UniversityFormResponse> GetSortedForms(List<UniversityFormResponse> allForms, string sortBy, 
        SortOrderOptions sortOrderOptions);
        UniversityFormResponse UpdateForms(UniversityFormUpdateRequest? universityFormUpdateRequest);
        bool DeleteForm(Guid? formId);
    }
}
