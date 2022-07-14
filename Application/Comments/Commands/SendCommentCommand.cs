using MediatR;

namespace Application.Comments.Commands
{
    public class SendCommentCommand : IRequest<SendCommentResponseDto>
    {
        public CommentDto Comment { get; set; }
        public SendCommentCommand(CommentDto comment)
        {
            Comment = comment;
        }
    }
}
