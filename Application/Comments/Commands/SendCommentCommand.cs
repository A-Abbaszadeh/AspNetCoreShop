using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
