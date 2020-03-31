using FluentValidation;

namespace FoxHound.App.Blogs.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog Id is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(128).WithMessage("Title must be less than or equal to {MaxLength} characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }
}