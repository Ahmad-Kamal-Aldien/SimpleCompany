using Compalny.R.PL.FunctionHelper;
using Compalny.R.PL.ViewModels;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;

namespace Compalny.R.PL.Controllers
{
    public class AccountController : Controller
    {
		public UserManager<ApplicationUser> _UserManager { get; }
		public SignInManager<ApplicationUser> _SignManager { get; }

		public AccountController(UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> SignManager
            )
        {
            userManager =_UserManager ;
            _SignManager = SignManager;


        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        
        {
            return View();
        }


		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
		{
            if(ModelState.IsValid)
            {


            var user=await _UserManager.FindByNameAsync(signUpViewModel.UserName);
                if(user is null)
                {
                    user = new ApplicationUser()
                    {
                        UserName = signUpViewModel.UserName,
                        Email = signUpViewModel.Email,
                        Firstname= signUpViewModel.FirstName,
                        Lastname= signUpViewModel.LastName,
						IsAgree= signUpViewModel.IsAgree,

					};
                  var result=await _UserManager.CreateAsync(user, signUpViewModel.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,item.Description);
                    }
                }

                ModelState.AddModelError(string.Empty, "User Name Exists");

            }
            return View(signUpViewModel);
		}
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if(ModelState.IsValid)
            {
                var user=await _UserManager.FindByEmailAsync(signInViewModel.Email);
                if(user is not null)
                {
                 bool flag= await _UserManager.CheckPasswordAsync(user, signInViewModel.Password);
                    if (flag)
                    {
                     var res=await   _SignManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, false);
                        if(res.Succeeded)
                        {
                            return RedirectToAction("index", "home");

                        }
                    }
                    
                }
                ModelState.AddModelError(string.Empty,"invaid Login");
            }
            return View();
        }

        public new async Task<IActionResult> SignOut()
        {
           await _SignManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
		{
            if (ModelState.IsValid)
            {
              var res=await  _UserManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (res is not null)
                {
                 var token=await   _UserManager.GeneratePasswordResetTokenAsync(res);
                    //Make Url
                    var url= Url.Action("ResetPass", "Acount", new {email=forgetPasswordViewModel.Email,token=token},
                        Request.Scheme
                        );
                    var email = new Company.R.DAL.Models.Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = ",,,,",
                        Body = url
					};
					EmailSetting.SendEmail(email);

                    return RedirectToAction(nameof(CheckEmail));
				}
                ModelState.AddModelError(string.Empty,"Invalid ");
            }

			return View(forgetPasswordViewModel);
		}

        [HttpGet] 
        public IActionResult CheckEmail()
        {
            return View();
        }

		[HttpGet]
		public IActionResult CheckResetPass(string email,string token)
		{
            TempData["email"] = email;
            TempData["token"] = token;
			return View();
		}

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
		public async Task<IActionResult> CheckResetPass(ResetViewModel resetViewModel)
		{
            if(ModelState.IsValid)
            {
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
                var user =await _UserManager.FindByEmailAsync(email);
                if(user != null)
                {
				var res=await	_UserManager.ResetPasswordAsync(user,token,resetViewModel.Password);
                    if(res.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
				}
			}
            ModelState.AddModelError(string.Empty,"Invalid");
			return View();
		}

	}
}
