using FoxHound.App.Blogs.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> Create(CreateCommentCommand command)
        {
            int commentId = await _mediator.Send(command);
            return commentId;
        }
    }
}