using System;

namespace Lime.Domain
{
    public class BusyTime
    {
        public string EmployeeId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
