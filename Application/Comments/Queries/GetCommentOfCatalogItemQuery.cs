using Application.Interfaces.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments.Queries
{
    public class GetCommentOfCatalogItemQuery:IRequest<List<GetCommentDto>>
    {
        public int CatalogItemId { get; set; }
    }

    public class GetCommentOfCatalogItemQueryHandler : IRequestHandler<GetCommentOfCatalogItemQuery, List<GetCommentDto>>
    {
        private readonly IDatabaseContext _context;

        public GetCommentOfCatalogItemQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<List<GetCommentDto>> Handle(GetCommentOfCatalogItemQuery request, CancellationToken cancellationToken)
        {
            var comments = _context.CatalogItemComments.Where(c => c.CatalogItemId == request.CatalogItemId)
                .Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Comment = c.Comment
                }).ToList();
            return Task.FromResult(comments);
        }
    }

    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
