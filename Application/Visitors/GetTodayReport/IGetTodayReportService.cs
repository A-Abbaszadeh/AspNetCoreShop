using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors.GetTodayReport
{
    public interface IGetTodayReportService
    {
        ResultTodayReportDto Execute();
    }

    public class GetTodayReportService : IGetTodayReportService
    {
        private readonly IVisitorDbContext<Visitor> _visitorDbContext;
        private readonly IMongoCollection<Visitor> _visitorCollection;
        public GetTodayReportService(IVisitorDbContext<Visitor> visitorDbContext)
        {
            _visitorDbContext = visitorDbContext;
            _visitorCollection = _visitorDbContext.GetCollection();
        }

        public ResultTodayReportDto Execute()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(1);

            var todayPageViewCount = _visitorCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).LongCount();
            var todayVisitorCount = _visitorCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).GroupBy(p => p.VisitorId).LongCount();
            var totalPageViewCount = _visitorCollection.AsQueryable().LongCount();
            var totalVisitorCount = _visitorCollection.AsQueryable().GroupBy(p => p.VisitorId).LongCount();

            return new ResultTodayReportDto
            {
                GeneralState = new GeneralStateDto
                {
                    TotalPageViews = totalPageViewCount,
                    TotalVisitors = totalVisitorCount,
                    PageViewPerVisit = totalPageViewCount / totalVisitorCount
                },
                TodayState = new TodayStateDto
                {
                    PageViews = todayPageViewCount,
                    Visitors = todayVisitorCount,
                    PageViewPerVisit = todayPageViewCount / todayVisitorCount
                }
            };
        }
    }

    public class ResultTodayReportDto
    {
        public GeneralStateDto GeneralState { get; set; }
        public TodayStateDto TodayState { get; set; }
    }
    public class GeneralStateDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewPerVisit { get; set; }
    }
    public class TodayStateDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float PageViewPerVisit { get; set; }
    }
}
