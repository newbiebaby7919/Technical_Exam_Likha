using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factories;

namespace Sprout.Exam.WebApp
{
    public static class StaticEmployees
    {
        private static EmployeeFactory employeeFactory = new EmployeeFactory();

        public static List<EmployeeDto> ResultList = new()
        {
            employeeFactory.CreateEmployee("Jane Doe", new DateTime(1993, 03, 25), "123215413", 1),
            employeeFactory.CreateEmployee("John Doe", new DateTime(1993, 05, 28), "957125412", 2)
        };
    }
}
