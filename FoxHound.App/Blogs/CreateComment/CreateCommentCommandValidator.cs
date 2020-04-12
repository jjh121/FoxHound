using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxHound.App.Blogs.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post Id is required");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MaximumLength(20).WithMessage("Author must be less than or equal to {MaxLength} characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }
}