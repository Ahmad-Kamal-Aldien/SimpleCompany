using Company.R.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Compalny.R.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }

        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^01[0-2]\\d{1,8}$")]
        public string PhoneNumber { get; set; }

        public int? Age { get; set; }
        [DataType(DataType.Currency)]


        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public DateTime HiringDate { get; set; }
        public DateTime? DateOfCreation { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public int Dept_id { get; set; }


        public virtual Department? Department { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName {  get; set; }

    }
}
