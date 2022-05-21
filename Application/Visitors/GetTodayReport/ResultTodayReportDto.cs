using System.Collections.Generic;

namespace Application.Visitors.GetTodayReport
{
    public class ResultTodayReportDto
    {
        public GeneralStateDto GeneralState { get; set; }
        public TodayStateDto TodayState { get; set; }
        public VisitCountDto VisitPerHour { get; set; }
        public List<VisitorDto> Visitors { get; set; }
    }
}
