using FluentValidation.Results;
using MediaNetwork.Common;
using MediaNetwork.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.ViewModels
{
    public class RegisterVIewModel : BaseViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<string> FirstName { get; set; } = new MProp<string>();
        public MProp<string> LastName { get; set; } = new MProp<string>();
        public DateTime DateOfBirth { get; set; } 

        public RegisterVIewModel()
        {
            Email.OnValueChanged = Validate;
            Password.OnValueChanged = Validate;
            FirstName.OnValueChanged = Validate;
            LastName.OnValueChanged = Validate;

            Email.Value = "";
            Password.Value = "";
            FirstName.Value = "";
            LastName.Value = "";
        }
        public bool IsRegisterBtnEneblaed => !Email.HasError && !Password.HasError && !FirstName.HasError && !LastName.HasError;
        private void Validate()
        {
            RegisterViewModelValidator validator = new RegisterViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                var passwordError = result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("Password"));

                if (passwordError != null)
                {
                    Password.Error = passwordError.ErrorMessage;
                }
                else
                {
                    Password.Error = "";
                }

                var emailError = result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("Email"));

                if (emailError != null)
                {
                    Email.Error = emailError.ErrorMessage;
                }
                else
                {
                    Email.Error = "";
                }

                var firstNameError = result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("FirstName"));

                if (firstNameError != null)
                {
                    FirstName.Error = firstNameError.ErrorMessage;
                }
                else
                {
                    FirstName.Error = "";
                }

                var lastNameError = result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("LastName"));

                if (lastNameError != null)
                {
                    LastName.Error = lastNameError.ErrorMessage;
                }
                else
                {
                    LastName.Error = "";
                }
            }
            else
            {
                FirstName.Error = "";
                LastName.Error = "";
                Email.Error = "";
                Password.Error = "";
            }
            NotifyPropertyChanged(nameof(IsRegisterBtnEneblaed));
        }
    }
}
