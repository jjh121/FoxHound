using FluentValidation;
using FoxHound.App.Blogs.Common;
using FoxHound.App.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoxHound.App.Blogs.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        private readonly IFoxHoundData _foxHoundData;

        public UpdateBlogCommandValidator(IFoxHoundData foxHoundData)
        {
            _foxHoundData = foxHoundData;

            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog Id is required");

            RuleFor(x => x).SetValidator(new CommonBlogCommandValidator());

            RuleFor(x => x.Owner)
                .MustAsync(OwnerNotAlreadyInUse).WithMessage("Owner already in use for a Blog");
        }

        private async Task<bool> OwnerNotAlreadyInUse(UpdateBlogCommand command, string ownerName, CancellationToken cancellationToken)
        {
            bool isOwnerInUse = await _foxHoundData.Blogs.AnyAsync(x => x.BlogId != command.BlogId && x.Owner.ToUpper() == ownerName.ToUpper(), cancellationToken);

            return !isOwnerInUse;
        }
    }
}