using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Comments.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGetCatalogItemPDPService _getCatalogItemPDPService;
        private readonly IMediator _mediator;
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;
        public ProductController(
            IGetCatalogItemPLPService getCatalogItemPLPService,
            IGetCatalogItemPDPService getCatalogItemPِDPService,
            IMediator mediator)
        {
            _getCatalogItemPLPService = getCatalogItemPLPService;
            _getCatalogItemPDPService = getCatalogItemPِDPService;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetCatalogPLPRequestDto request)
        {
            return Ok(_getCatalogItemPLPService.Execute(request));
        }

        [HttpGet]
        [Route("DPD")]
        public IActionResult Get([FromQuery] string slug)
        {
            return Ok(_getCatalogItemPDPService.Execute(slug));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CommentDto comment)
        {
            SendCommentCommand sendComment = new SendCommentCommand(comment);
            var result = _mediator.Send(sendComment).Result;
            return Ok(result);
        }
    }
}
