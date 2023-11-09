using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factories
{
    public class EmployeeFactory
    {
        public EmployeeDto CreateEmployee(string fullName, DateTime birthdate, string tin, int typeId)
        {
            return new EmployeeDto()
            {
                FullName = fullName,
                Birthdate = birthdate,
                Tin = tin,
                TypeId = typeId,
            };
        }

        public RegularEmployeeDto CreateRegularEmployee(EmployeeDto employee)
        {
            return new RegularEmployeeDto()
            {
                FullName = employee.FullName,
                Birthdate = employee.Birthdate,
                Tin = employee.Tin,
                TypeId = (int)EmployeeType.Regular,
            };
        }

        public ContractualEmployeeDto CreateContractualEmployee(EmployeeDto employee)
        {
            return new ContractualEmployeeDto()
            {
                FullName = employee.FullName,
                Birthdate = employee.Birthdate,
                Tin = employee.Tin,
                TypeId = (int)EmployeeType.Contractual,
            };
        }
    }
}
