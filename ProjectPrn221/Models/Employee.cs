using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrn221.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Accounts = new HashSet<Account>();
            Orders = new HashSet<Order>();
        }
        public int EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
       
        public int? DepartmentId { get; set; }

        public string? Title { get; set; }
        public string? TitleOfCourtesy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime? BirthDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
