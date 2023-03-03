using System.Collections.Generic;

namespace api.Domain.VM.Shared
{
    public abstract class BaseViewModel
    {
        public List<Error>? Errors { get; private set; } 

        public void SetErrors(List<Error> errors)
        {
            Initialize();

            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }

        public void AddError(string errorMessage, string propertyName)
        {
            Initialize();

            var error = new Error()
            {
                ErrorMessage = errorMessage,
                PropertyName = propertyName,
            };

            Errors.Add(error);
        }

        private void Initialize()
        {
            if (Errors == null) Errors = new List<Error>();
        }
    }

    public class Error
    {
        public string PropertyName { get; set;}
        public string ErrorMessage { get; set; }
    }
}
