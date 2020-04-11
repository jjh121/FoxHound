using FluentValidation;
using FoxHound.App.Blogs.Common;

namespace FoxHound.App.Blogs.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog Id is required");

            RuleFor(x => x).SetValidator(new CommonBlogCommandValidator());
        }
    }
}