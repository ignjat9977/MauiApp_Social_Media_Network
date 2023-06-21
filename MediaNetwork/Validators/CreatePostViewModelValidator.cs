using FluentValidation;
using MediaNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Validators
{
    internal class CreatePostViewModelValidator:AbstractValidator<CreatePostPageViewModel>
    {
        public CreatePostViewModelValidator()
        {
            RuleFor(x => x.Title.Value)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Title is required field!")
                .MinimumLength(3)
                .WithMessage("Title must have at least 3 characters!")
                .MaximumLength(50)
                .WithMessage("Title must have less or equal 50 characters!");

            RuleFor(x => x.Content.Value)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Content is required field!")
                .MinimumLength(10)
                .WithMessage("Content must have at least 10 characters!")
                .MaximumLength(1000)
                .WithMessage("Content must have less or equal 1000 characters!");
        }
    }
}
