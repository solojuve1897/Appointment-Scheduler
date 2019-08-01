using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Data
{
    public interface IBusyTimeRepository
    {
        IList<BusyTime> GetBusyTimes(string[] employeeIds);
    }
}
