using FluentValidation;

namespace FoxHound.App.Blogs.Common
{
    public class CommonBlogCommandValidator : AbstractValidator<IBlogCommand>
    {
        public CommonBlogCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(128).WithMessage("Title must be less than or equal to {MaxLength} characters");

            RuleFor(x => x.Owner)
                .NotEmpty().WithMessage("Owner is required")
                .MaximumLength(20).WithMessage("Owner must be less than or equal to {MaxLength} characters");
        }
    }
}
