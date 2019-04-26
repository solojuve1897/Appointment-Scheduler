using Lime.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lime.Data
{
    public class DataContext : IDataContext
    {
        private IList<Employee> _employees;
        private IList<BusyTime> _busyTimes;

        public IList<Employee> Employees
        {
            get { return _employees; }
        }

        public IList<BusyTime> BusyTimes
        {
            get { return _busyTimes; }
        }

        public DataContext()
        {
            _employees = new List<Employee>();
            _busyTimes = new List<BusyTime>();
            LoadData();
        }

        private void LoadData ()
        {
            var streamReader = new StreamReader(new FileStream(@"App_Data/freebusy.txt", FileMode.Open));

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var values = line.Split(';');

                if (values.Length < 2)
                    continue;

                if (values.Length == 2)
                {
                    var employee = new Employee
                    {
                        Id = values[0],
                        Name = values[1]
                    };

                    _employees.Add(employee);
                }
                else
                {
                    var busyTime = new BusyTime
                    {
                        EmployeeId = values[0],
                        StartTime = DateTimeOffset.Parse(values[1]),
                        EndTime = DateTimeOffset.Parse(values[2])
                    };

                    _busyTimes.Add(busyTime);
                }
            }

            streamReader.Dispose();
        }
    }
}
