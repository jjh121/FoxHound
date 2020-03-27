using FluentValidation;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty().WithMessage("Owner is required");
        }
    }
}