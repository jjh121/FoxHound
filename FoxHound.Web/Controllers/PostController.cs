using FoxHound.App.Blogs.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoxHound.Web.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> Create(CreatePostCommand command)
        {
            int postId = await _mediator.Send(command);
            return postId;
        }
    }
}