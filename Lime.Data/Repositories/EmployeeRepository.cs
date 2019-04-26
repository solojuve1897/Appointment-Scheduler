using System.Collections.Generic;
using System.Linq;
using Lime.Domain;

namespace Lime.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDataContext _dataContext;

        public EmployeeRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IList<Employee> GetEmployees(string query)
        {
            return _dataContext.Employees.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToList();
        }
    }
}
