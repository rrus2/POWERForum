using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POWERForum.Context;
using POWERForum.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POWERForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userVM = new ApplicationUserViewModel() { ApplicationUser = user, Roles = roles };
            if (user == null)
                return NotFound();
            return Ok(userVM);
        }

        [HttpPost("loginuser")]
        public async Task<ActionResult<ApplicationUserViewModel>> LoginUser()
        {
            var username = Request.Form["username"][0];
            var password = Request.Form["password"][0];

            if (username == null || username == string.Empty)
                return BadRequest();
            if (password == null || password == string.Empty)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(username);
            Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
            if(user != null)
                result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            var roles = await _userManager.GetRolesAsync(user);
            var userVM = new ApplicationUserViewModel() { ApplicationUser = user, Roles = roles };
            if (result.Succeeded)
                return Ok(userVM);
            else
                return BadRequest(result);
        }

        // POST api/<UsersController>
        [HttpPost("createuser")]
        public async Task<ActionResult<ApplicationUserViewModel>> PostUser()
        {
            var email = Request.Form["email"][0];
            var birthdateToConvert = Request.Form["birthdate"][0];
            DateTime birthdate;
            var password = Request.Form["password"][0];
            var repeatpassword = Request.Form["repeatpassword"][0];

            if (email == null || email == string.Empty)
                return BadRequest("Bad email");
            if (!DateTime.TryParse(birthdateToConvert, out birthdate))
                return BadRequest("Invalid birthdate");
            if (password == null || password == string.Empty)
                return BadRequest("Invalid password");
            if (repeatpassword == null || repeatpassword == string.Empty)
                return BadRequest("Invalid repeat password");
            if (password != repeatpassword)
                return BadRequest("Password and repeat password do not match");

            var user = new ApplicationUser()
            {
                Birthdate = birthdate,
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (_context.Users.Count() == 1)
                    await _userManager.AddToRoleAsync(user, "Admin");
                else
                    await _userManager.AddToRoleAsync(user, "User");
                var roles = await _userManager.GetRolesAsync(user);
                var userVM = new ApplicationUserViewModel() { ApplicationUser = user, Roles = roles };
                return CreatedAtAction("GetUser", new { id = userVM.ApplicationUser.Id }, userVM);
            }
            else
                return BadRequest(result.Errors);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationUser>> PutUser(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser(string id)
        {
            if (id == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return Ok(user);
            else
                return BadRequest();
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
