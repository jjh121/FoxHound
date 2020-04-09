using FluentValidation;

namespace FoxHound.App.Blogs.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog Id is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(128).WithMessage("Title must be less than or equal to {MaxLength} characters");

            RuleFor(x => x.Owner)
                .NotEmpty().WithMessage("Owner is required")
                .MaximumLength(20).WithMessage("Owner must be less than or equal to {MaxLength} characters");
        }
    }
}