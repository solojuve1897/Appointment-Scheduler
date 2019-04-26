using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Business
{
    public interface IEmployeeLogic
    {
        IList<Employee> GetEmployees(string query);
    }
}