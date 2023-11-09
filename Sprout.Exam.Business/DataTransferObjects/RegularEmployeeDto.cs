using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class RegularEmployeeDto : EmployeeDto
    {
        public override decimal CalculatedSalary(decimal absentDays)
        {
            decimal totalNetSalary = 20000M - this.DeductedSalaryAfterAbsences(absentDays) - this.SalaryTax();
            return Math.Round(totalNetSalary > 0 ? totalNetSalary : 0, 2);
        }

        public decimal DeductedSalaryAfterAbsences(decimal absentDays)
        {
            if (absentDays < 0) return 0.00M;
            return absentDays * (20000M / Constants.WORKING_DAYS);
        }

        public decimal SalaryTax()
        {
            return 20000M * Constants.TAX_PERCENTAGE;
        }
    }

    public static class Constants
    {
        public const decimal TAX_PERCENTAGE = 0.12M;
        public const int WORKING_DAYS = 22;
    }
}
