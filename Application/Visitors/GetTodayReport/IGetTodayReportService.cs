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
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.AddDays(1);

            var todayPageViewCount = _visitorCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).LongCount();
            var todayVisitorCount = _visitorCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).GroupBy(p => p.VisitorId).LongCount();
            var totalPageViewCount = _visitorCollection.AsQueryable().LongCount();
            var totalVisitorCount = _visitorCollection.AsQueryable().GroupBy(p => p.VisitorId).LongCount();

            VisitCountDto visitPerHour = GetVisitPerHour();
            VisitCountDto visitPerDay = GetVisitPerDay();

            var visitors = _visitorCollection.AsQueryable()
                .OrderByDescending(p => p.Time)
                .Take(10)
                .Select(p => new VisitorDto
                {
                    Id = p.Id,
                    Ip = p.Ip,
                    CurrentLink = p.CurrentLink,
                    ReferrerLink = p.ReferrerLink,
                    Browser = p.Browser.Family,
                    OperatingSystem = p.OperatingSystem.Family,
                    IsSpider = p.Device.IsSpider,
                    Time = p.Time,
                    VisitorId = p.VisitorId
                }).ToList();

            return new ResultTodayReportDto
            {
                GeneralState = new GeneralStateDto
                {
                    TotalPageViews = totalPageViewCount,
                    TotalVisitors = totalVisitorCount,
                    PageViewPerVisit = GetAverage(totalPageViewCount, totalVisitorCount),
                    VisitPerDay = visitPerDay
                },
                TodayState = new TodayStateDto
                {
                    PageViews = todayPageViewCount,
                    Visitors = todayVisitorCount,
                    PageViewPerVisit = GetAverage(todayPageViewCount, todayVisitorCount)
                },
                VisitPerHour = visitPerHour,
                Visitors = visitors
            };
        }

        private VisitCountDto GetVisitPerHour()
        {
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.AddDays(1);

            var todayPageViewList = _visitorCollection.AsQueryable()
                            .Where(p => p.Time >= start && p.Time < end).Select(p => new { p.Time }).ToList();

            VisitCountDto visitperHour = new VisitCountDto
            {
                // 24 hours in one day
                Display = new string[24],
                Value = new int[24]
            };

            for (int i = 0; i < 24; i++)
            {
                visitperHour.Display[i] = $"h-{i}";
                visitperHour.Value[i] = todayPageViewList.Where(p => p.Time.Hour == i).Count();
            }

            return visitperHour;
        }

        private VisitCountDto GetVisitPerDay()
        {
            DateTime start = DateTime.Now.AddDays(-30);
            DateTime end = DateTime.Now.AddDays(1);

            var monthPageViewList = _visitorCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).Select(p => new { p.Time }).ToList();

            VisitCountDto visitPerDay = new VisitCountDto
            {
                // 31 days in one month
                Display = new string[31],
                Value = new int[31]
            };

            for (int i = 0; i < 31; i++)
            {
                DateTime currentDay = DateTime.Now.AddDays(i+1 * (-1));
                visitPerDay.Display[i] = (i+1).ToString(); // 1 to 31
                visitPerDay.Value[i] = monthPageViewList.Where(p => p.Time.Date == currentDay.Date).Count();
            }

            return visitPerDay;
        }

        private float GetAverage(long visitPage, long visitor)
        {
            if (visitor == 0)
            {
                return 0;
            }
            return visitPage / visitor;
        }
    }

    public class ResultTodayReportDto
    {
        public GeneralStateDto GeneralState { get; set; }
        public TodayStateDto TodayState { get; set; }
        public VisitCountDto VisitPerHour { get; set; }
        public List<VisitorDto> Visitors { get; set; }
    }
    public class GeneralStateDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewPerVisit { get; set; }
        public VisitCountDto VisitPerDay { get; set; }
    }
    public class TodayStateDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float PageViewPerVisit { get; set; }
    }
    public class VisitCountDto
    {
        public string[] Display { get; set; }
        public int[] Value { get; set; }
    }

    public class VisitorDto
    {
        public string Id { get; set; }
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string ReferrerLink { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsSpider { get; set; }
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }
    }
}
