using Company.R.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Compalny.R.PL.ViewModels
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]

        public string Code { get; set; }

        public DateTime DOPCreation { get; set; }

        public ICollection<Employee>? Employees { get; set; }

    }
}
