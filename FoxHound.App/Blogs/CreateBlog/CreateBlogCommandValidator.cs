using FluentValidation;
using FoxHound.App.Blogs.Common;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x => x).SetValidator(new CommonBlogCommandValidator());
        }
    }
}