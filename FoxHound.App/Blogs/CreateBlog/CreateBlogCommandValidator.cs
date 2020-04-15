using FluentValidation;
using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        private readonly IFoxHoundData _foxHoundData;

        public CreateBlogCommandValidator(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;

            RuleFor(x => x).SetValidator(new CommonBlogCommandValidator());

            RuleFor(x => x.Owner)
                .MustAsync(OwnerNotAlreadyInUse).WithMessage("Owner already in use for a Blog");
        }

        private async Task<bool> OwnerNotAlreadyInUse(CreateBlogCommand command, string ownerName, CancellationToken cancellationToken)
        {
            bool isOwnerInUse = await _foxHoundData.Blogs.AnyAsync(x => x.Owner.ToUpper() == ownerName.ToUpper(), cancellationToken);

            return !isOwnerInUse;
        }
    }
}