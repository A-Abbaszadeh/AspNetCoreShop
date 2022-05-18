using Application.Visitors.GetTodayReport;
using Application.Visitors.OnlineVisitors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.Visitor
{
    public class IndexModel : PageModel
    {
        private readonly IGetTodayReportService _getTodayReportService;
        private readonly IOnlineVisitorService _onlineVisitorService;
        public ResultTodayReportDto ResultTodayReport;
        public int OnlineVisitorCount;
        public IndexModel(IGetTodayReportService getTodayReportService, IOnlineVisitorService onlineVisitorService)
        {
            _getTodayReportService = getTodayReportService;
            _onlineVisitorService = onlineVisitorService;
        }

        public void OnGet()
        {
            ResultTodayReport = _getTodayReportService.Execute();
            OnlineVisitorCount = _onlineVisitorService.GetCount();
        }
    }
}
