using FluentValidation;
using MediaNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterVIewModel>
    {
        public RegisterViewModelValidator()
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

            RuleFor(x => x.LastName.Value).Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("LastName cant be empty!")
               .MinimumLength(3)
               .WithMessage("LastName must be minimum 3 caracters")
               .MaximumLength(20)
               .WithMessage("LastName must be Maximun 20 caracters");

            RuleFor(x => x.FirstName.Value).Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithMessage("FirstName cant be empty!")
              .MinimumLength(3)
              .WithMessage("FirstName must be minimum 3 caracters")
              .MaximumLength(20)
              .WithMessage("FirstName must be Maximun 20 caracters");
        }
    }
}