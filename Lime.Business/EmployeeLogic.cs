using System.Collections.Generic;
using Lime.Data;
using Lime.Domain;

namespace Lime.Business
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeLogic(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IList<Employee> GetEmployees(string query)
        {
            return _employeeRepository.GetEmployees(query);
        }
    }
}
