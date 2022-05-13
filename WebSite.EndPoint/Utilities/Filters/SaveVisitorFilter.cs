using Application.Visitors;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace WebSite.EndPoint.Utilities.Filters
{
    public class SaveVisitorFilter : IActionFilter
    {
        private readonly ISaveVisitorInfoService _saveVisitorInfoService;
        public SaveVisitorFilter(ISaveVisitorInfoService saveVisitorInfoService)
        {
            _saveVisitorInfoService = saveVisitorInfoService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var controlerName = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName;

            var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo = uaParser.Parse(userAgent);

            var referrerLink = context.HttpContext.Request.Headers["Referrer"].ToString();
            var currentLink = context.HttpContext.Request.Path;

            _saveVisitorInfoService.Execute(new RequestSaveVisitorInfoDto
            {
                Ip = ip,
                CurrentLink = currentLink,
                ReferrerLink = referrerLink,
                Method = request.Method,
                Protocol = request.Protocol,
                PhysicalPath = $"{controlerName}/{actionName}",
                Browser = new VisitorVersionDto
                {
                    Family = clientInfo.UA.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
                },
                OperatingSystem = new VisitorVersionDto
                {
                    Family = clientInfo.OS.Family,
                    Version = $"{clientInfo.OS.Major}.{clientInfo.OS.Minor}.{clientInfo.OS.Patch}"
                },
                Device = new DeviceDto
                {
                    Brand = clientInfo.Device.Brand,
                    Family = clientInfo.Device.Family,
                    Model = clientInfo.Device.Model,
                    IsSpider = clientInfo.Device.IsSpider
                }
            });
        }
    }
}
