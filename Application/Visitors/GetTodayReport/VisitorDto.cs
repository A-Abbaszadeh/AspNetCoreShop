﻿using System;

namespace Application.Visitors.GetTodayReport
{
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