using FoxHound.App.Blogs.Common;
using FoxHound.App.Blogs.CreateBlog;
using FoxHound.App.Blogs.GetAllBlogs;
using FoxHound.App.Blogs.GetBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoxHound.Web.Controllers
{
    public class BlogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<BlogResult>> GetAll()
        {
            var result = await _mediator.Send(new GetAllBlogsQuery());
            return result;
        }

        [HttpGet("[action]/blogId:int")]
        public async Task<BlogResult> Get(int blogId)
        {
            var result = await _mediator.Send(new GetBlogQuery(blogId));
            return result;
        }

        [HttpPost("[action]")]
        public async Task<int> Create(CreateBlogCommand command)
        {
            int blogId = await _mediator.Send(command);
            return blogId;
        }
    }
}