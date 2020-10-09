using FluentValidation;
using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.EntityValidator
{
    public class NoteValidator : AbstractValidator<Note>
    {

        public NoteValidator()
        {
            RuleFor(n => n.Title)
                    .NotEmpty().WithMessage("Please specify title")
                    .MinimumLength(8).MaximumLength(32);

            RuleFor(n => n.Content)
                .NotEmpty().WithMessage("Please specify content")
                .MinimumLength(1).MaximumLength(10000);
        }

    }
}
