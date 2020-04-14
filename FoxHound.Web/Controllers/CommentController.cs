using FoxHound.App.Blogs.Common;
using FoxHound.App.Blogs.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoxHound.Web.Controllers
{
    public class CommentController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<CommentResult> Create(CreateCommentCommand command)
        {
            CommentResult comment = await _mediator.Send(command);
            return comment;
        }
    }
}