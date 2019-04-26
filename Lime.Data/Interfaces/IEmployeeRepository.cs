using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Data
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetEmployees(string query);
    }
}
