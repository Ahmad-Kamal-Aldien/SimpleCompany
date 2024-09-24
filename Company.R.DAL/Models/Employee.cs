using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.DAL.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
      
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^01[0-2]\\d{1,8}$")]
        public string PhoneNumber { get; set; }
        public string? ImageName { get; set; }


        public int? Age { get; set; }
        [DataType(DataType.Currency)]

        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public DateTime HiringDate { get; set; }
        public DateTime? DateOfCreation { get; set; }=DateTime.Now;

        [ForeignKey("Department")]
        public int Dept_id { get; set; }


        public  Department? Department { get; set; }

    }
}
