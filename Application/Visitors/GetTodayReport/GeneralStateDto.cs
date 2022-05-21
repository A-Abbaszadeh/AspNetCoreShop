namespace Application.Visitors.GetTodayReport
{
    public class GeneralStateDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewPerVisit { get; set; }
        public VisitCountDto VisitPerDay { get; set; }
    }
}
