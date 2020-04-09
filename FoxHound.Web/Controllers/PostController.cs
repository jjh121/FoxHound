using FoxHound.App.Blogs.Common;
using FoxHound.App.Blogs.CreatePost;
using FoxHound.App.Blogs.GetPost;
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

        [HttpPost("[action]/{postId:int}")]
        public async Task<PostResult> Get(int postId)
        {
            PostResult post = await _mediator.Send(new GetPostQuery(postId));
            return post;
        }
    }
}