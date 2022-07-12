using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Comments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class ProductController : Controller
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

        public IActionResult Index(GetCatalogPLPRequestDto request)
        {
            var data = _getCatalogItemPLPService.Execute(request);
            return View(data);
        }

        public IActionResult Details(string slug)
        {
            var data = _getCatalogItemPDPService.Execute(slug);
            return View(data);
        }

        public IActionResult SendComment(CommentDto comment, string slug)
        {
            SendCommentCommand sendComment = new SendCommentCommand(comment);
            var result = _mediator.Send(sendComment).Result;
            return RedirectToAction(nameof(Details), new { Slug = slug });
        }
    }
}
