using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lime.Domain;

namespace Lime.Data
{
    public class BusyTimeRepository : IBusyTimeRepository
    {
        private readonly IDataContext _dataContext;

        public BusyTimeRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IList<BusyTime> GetBusyTimes(string[] employeeIds)
        {
            return _dataContext.BusyTimes.Where(x => employeeIds.Contains(x.EmployeeId)).ToList();
        }
    }
}
