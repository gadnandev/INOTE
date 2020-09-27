using FluentValidation;
using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.EntityValidator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleSet("Login", () => {
                RuleFor(u => u.Username)
                    .NotEmpty().WithMessage("Please specify username");

                RuleFor(u => u.Password)
                    .NotEmpty().WithMessage("Please specify password");
            });

            RuleSet("Register", () => {
                RuleFor(u => u.Username)
                    .NotEmpty().WithMessage("Please specify username")
                    .MinimumLength(8).MaximumLength(32);

                RuleFor(u => u.Email)
                   .NotEmpty().WithMessage("Please specify email")
                   .EmailAddress();

                RuleFor(u => u.Password)
                    .NotEmpty().WithMessage("Please specify password")
                    .MinimumLength(8).MaximumLength(255);
            });
        }
    }
}
