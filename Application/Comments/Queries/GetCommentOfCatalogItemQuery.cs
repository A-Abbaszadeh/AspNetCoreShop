using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Comments.Queries
{
    public class GetCommentOfCatalogItemQuery:IRequest<List<GetCommentDto>>
    {
        public int CatalogItemId { get; set; }
    }
}
