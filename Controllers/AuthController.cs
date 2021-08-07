using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrisDashboardAPI.Models;
using PrisDashboardAPI.ViewModels;

namespace PrisDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Creating Employee User
        // Route - /api/Auth/Employee
        [Route("Employee")]
        [HttpPost]
        public async Task<ActionResult> Employee(EmployeeSignupVM model)
        {
            string currentDate = DateTime.Now.ToString("d/M/yyyy");

            var user = new User()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfilePic = model.ProfilePic,
                CreatedAt = currentDate,
                IsActive = false,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                // To Create a new Employee Role - Mudasir Ali
                IdentityRole newRole = new IdentityRole()
                {
                    Name = "Employee"
                };
                if (result.Succeeded)
                {
                    await _roleManager.CreateAsync(newRole);
                    var role = await _userManager.AddToRoleAsync(user, "Employee");
                }
                
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { succeeded = false, code = "ServerError", description = "Something went wrong in the Server !" });
            }
        }

    }
}