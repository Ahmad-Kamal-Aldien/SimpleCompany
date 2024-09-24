using Compalny.R.PL.ViewModels;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compalny.R.PL.Controllers
{
	public class UserController : Controller
	{
		public UserManager<ApplicationUser> _UserManager;
        public UserController(UserManager<ApplicationUser> UserManager)
        {
            _UserManager = UserManager;
        }
        public async Task<IActionResult> Index(string search)
		{
			var user =Enumerable.Empty<UserViewModel>();
			if (string.IsNullOrEmpty(search))
			{
				user = await _UserManager.Users.Select(x=> new UserViewModel()
				{
					Email=x.Email,
					FirstName=x.Firstname,
					LastName=x.Lastname,
					Id=x.Id,
					Roles=_UserManager.GetRolesAsync(x).Result
				}).ToListAsync();
			}
			else
			{
				user = await _UserManager.Users.Where(x=>x.Email.ToLower().Contains(search
					.ToLower())).Select(x=> new UserViewModel()
					{
						Email = x.Email,
						FirstName = x.Firstname,
						LastName = x.Lastname,
						Id = x.Id,
						Roles = _UserManager.GetRolesAsync(x).Result
					}).ToListAsync();
			}
			return View(user);
		}


		public async Task<IActionResult>Details(string id,string viewname = "Details") 
		{
			if (id is null) return BadRequest();
			var user= await _UserManager.FindByIdAsync(id);
			if(user is null){
				return NotFound();
			}
			var users = new UserViewModel()
			{
				Email = user.Email,
				FirstName = user.Firstname,
				LastName = user.Lastname,
				Id = user.Id,
				Roles = _UserManager.GetRolesAsync(user).Result
			};
			return View(viewname,users);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(string ?id) {
			return await Details(id,"Edit");
		}
		[HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string? id,UserViewModel userViewModel)
        {
			if(id != userViewModel.Id) { return BadRequest(); }
			if(ModelState.IsValid)
			{
				var user = await _UserManager.FindByIdAsync(id);
				if(user is null){
					return NotFound();
				}
				user.Firstname = userViewModel.FirstName;
				user.Lastname = userViewModel.LastName;
				user.Email = userViewModel.Email;

				await _UserManager.UpdateAsync(user);
				return RedirectToAction(nameof(Index));
			}
            return View(userViewModel);
        }

		[HttpGet]
		public async Task<IActionResult>Delete(string id)
		{
			return await Details(id,"Delete");
		}

		public async Task<IActionResult> Delete([FromRoute] string id,UserViewModel userViewModel)
		{
            if (id != userViewModel.Id)
			{
				return BadRequest();
			}
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }
                
                await _UserManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }



    }

}
