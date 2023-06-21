using FluentValidation.Results;
using MediaNetwork.Common;
using MediaNetwork.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();

        public LoginViewModel()
        {
            Password.OnValueChanged = Validate;
            Email.OnValueChanged = Validate;
            Email.Value = "";
            Password.Value = "";
        }
        public bool IsLoginButtonEnabled => !Email.HasError && !Password.HasError;
        private void Validate()
        {
            LoginViewModelValidator validator = new LoginViewModelValidator();

            ValidationResult valid = validator.Validate(this);

            if (!valid.IsValid)
            {
                var passwordError = valid.Errors.FirstOrDefault(x=>x.ErrorMessage.Contains("Password"));

                if(passwordError != null)
                {
                    Password.Error = passwordError.ErrorMessage;
                }
                else
                {
                    Password.Error = "";
                }

                var emailError = valid.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("Email"));

                if (emailError != null)
                {
                    Email.Error = emailError.ErrorMessage;
                }
                else
                {
                    Email.Error = "";
                }
            }
            else
            {
                Email.Error = "";
                Password.Error = "";
            }
            NotifyPropertyChanged(nameof(IsLoginButtonEnabled));
        } 
    }
}
