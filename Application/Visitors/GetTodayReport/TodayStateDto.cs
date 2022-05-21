namespace Application.Visitors.GetTodayReport
{
    public class TodayStateDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float PageViewPerVisit { get; set; }
    }
}
