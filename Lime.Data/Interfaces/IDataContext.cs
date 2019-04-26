using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Data
{
    public interface IDataContext
    {
        IList<Employee> Employees { get; }
        IList<BusyTime> BusyTimes { get; }
    }
}
