using Application.Interfaces.Contexts;
using Domain.Catalogs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments.Commands
{
    public class SendCommentHandler : IRequestHandler<SendCommentCommand, SendCommentResponseDto>
    {
        private readonly IDatabaseContext _context;

        public SendCommentHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<SendCommentResponseDto> Handle(SendCommentCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = _context.CatalogItems.Find(request.Comment.CatalogItemId);

            CatalogItemComment comment = new CatalogItemComment
            {
                Title = request.Comment.Title,
                Email = request.Comment.Email,
                Comment = request.Comment.Comment,
                CatalogItem = catalogItem,
            };

            var result = _context.CatalogItemComments.Add(comment);
            _context.SaveChanges();

            return Task.FromResult(new SendCommentResponseDto
            {
                Id = result.Entity.Id
            });
        }
    }

}
