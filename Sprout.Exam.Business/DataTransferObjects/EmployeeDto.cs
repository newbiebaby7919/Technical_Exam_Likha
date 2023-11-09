using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        [Column("TIN")]
        public string Tin { get; set; }
        [Column("EmployeeTypeId")]
        public int TypeId { get; set; }
        public virtual decimal CalculatedSalary(decimal days) { return 20000M; }
    }
}
