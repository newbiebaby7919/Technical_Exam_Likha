using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class ContractualEmployeeDto : EmployeeDto
    {
        public override decimal CalculatedSalary(decimal workedDays)
        {
            if (workedDays < 0) { return 0.00M; }
            return Math.Round(500M * workedDays, 2);
        }
    }
}
