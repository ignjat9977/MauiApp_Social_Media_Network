using FluentValidation;
using MediaNetwork.Business;
using MediaNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email.Value).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email cant be empty!")
                .EmailAddress()
                .WithMessage("Email address is not in right format!");

            var regexPassword = @"^[A-Za-z0-9!@#$%%&*()+_(]{8,20}$";

            RuleFor(x => x.Password.Value).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password cant be empty!")
                .Matches(regexPassword)
                .WithMessage("Password is not in right format!");

        }
    }
}
