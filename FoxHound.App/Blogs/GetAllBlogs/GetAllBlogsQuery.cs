using FoxHound.App.Blogs.Common;
using MediatR;
using System.Collections.Generic;

namespace FoxHound.App.Blogs.GetAllBlogs
{
    public class GetAllBlogsQuery : IRequest<List<BlogResult>>
    {
    }
}