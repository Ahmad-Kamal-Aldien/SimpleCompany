using System.ComponentModel.DataAnnotations;

namespace Compalny.R.PL.ViewModels
{
	public class ResetViewModel
	{
		public string Password { get; set; }

		[Compare(nameof(SignUpViewModel.Password), ErrorMessage = "Not Match Password")]
		public string ConfirmedPassword { get; set; }
	}
}
