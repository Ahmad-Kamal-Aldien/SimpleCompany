using System.ComponentModel.DataAnnotations;

namespace Compalny.R.PL.ViewModels
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        [Compare(nameof(SignUpViewModel.Password),ErrorMessage ="Not Match Password")]
        public string ConfirmedPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
