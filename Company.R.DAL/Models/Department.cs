using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.DAL.Models
{
    public class Department:BaseEntity
    {
     
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]

        public string Code {  get; set; }

        public DateTime DOPCreation {  get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
