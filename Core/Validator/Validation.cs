using api.Domain.VM.Shared;
using FluentValidation.Results;

namespace Core.Validator
{
    public static class Validation
    {
        public static void AddErrors(dynamic model, ValidationResult results)
        {
            if (!results.IsValid)
            {
                var errors = results.Errors.Select(x => new Error { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage }).ToList();
                model.SetErrors(errors);
            }
            
        }
    }
}
