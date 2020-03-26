using FoxHound.App.Blogs.CreateBlog;
using FoxHound.App.Blogs.GetAllBlogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoxHound.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
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

        [HttpPost("[action]")]
        public async Task<int> Create(CreateBlogCommand command)
        {
            int blogId = await _mediator.Send(command);
            return blogId;
        }
    }
}