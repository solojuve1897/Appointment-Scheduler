using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Data
{
    public interface IBusyTimeRepository
    {
        /// <summary>
        /// Retrieves all busy-dates for a list of employeeIds
        /// </summary>
        IList<BusyTime> GetBusyTimes(string[] employeeIds);
    }
}
