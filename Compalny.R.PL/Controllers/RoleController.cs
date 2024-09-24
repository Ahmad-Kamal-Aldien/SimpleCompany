using Compalny.R.PL.ViewModels;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Compalny.R.PL.Controllers
{
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _RoleManager;
        public UserManager<ApplicationUser> _UserManager ;
        public RoleController(RoleManager<IdentityRole> RoleManager,UserManager<ApplicationUser>userManager)
        {
            _RoleManager = RoleManager;
            _UserManager = userManager;
        }
        public async Task<IActionResult> Index(string search)
        {
            var role = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(search))
            {
                role = await _RoleManager.Roles.Select(x => new RoleViewModel()
                {
                   id = x.Id,
                   RoleName=x.Name
                }).ToListAsync();
            }
            else
            {
                role = await _RoleManager.Roles.Where(x => x.Name.ToLower().Contains(search
                    .ToLower())).Select(x => new RoleViewModel()
                    {
                        
                        id = x.Id,
                        RoleName = x.Name
                    }).ToListAsync();
            }
            return View(role);
        }


        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null) return BadRequest();
            var role = await _RoleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            var roles = new RoleViewModel()
            {
                id=role.Id,
                RoleName=role.Name
            };
            return View(viewname, role);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                var role = await _RoleManager.FindByIdAsync(id);
                if (role is null)
                {
                    return NotFound();
                }
                role.Name = roleViewModel.RoleName;
               

                await _RoleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel userViewModel)
        {
            if (id != userViewModel.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var user = await _RoleManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }

                await _RoleManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                var role = new IdentityRole() { Name=roleViewModel.RoleName
                
                };
            await    _RoleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemove(string roleid)
        {
            var role=await _RoleManager.FindByIdAsync(roleid);
            if (role is null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = roleid;
            var userInRole = new List<UserInRoleViewModel>();
            var user=await _UserManager.Users.ToListAsync();

            foreach (var useritem in user)
            {
                var usInRol = new UserInRoleViewModel()
                {
                    UserId = useritem.Id,

                    UserName = useritem.UserName
                };
                if (await _UserManager.IsInRoleAsync(useritem,role.Name))
                    {
                    usInRol.IsSelectes = true;
                }
                else
                {
                    usInRol.IsSelectes = false;

                }
                userInRole.Add(usInRol);
            }
            
            return View(userInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemove(string roleid,List<UserInRoleViewModel> userInRoleViewModels)
        {
            var role = await _RoleManager.FindByIdAsync(roleid);
            if (role is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach (var item in userInRoleViewModels)
                {
                    var appuser = await _UserManager.FindByIdAsync(item.UserId);
                    if (item.IsSelectes && ! await _UserManager.IsInRoleAsync(appuser,role.Name))
                    {
                      await  _UserManager.AddToRoleAsync(appuser,role.Name);
                    }else if (item.IsSelectes && await _UserManager.IsInRoleAsync(appuser, role.Name))
                    {
                       await _UserManager.RemoveFromRoleAsync(appuser, role.Name);

                    }
                }
                return RedirectToAction(nameof(Edit),new { id=roleid});


            }
            return View(userInRoleViewModels);
        }

    }
}
