﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POWERForum.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POWERForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            if (id == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser()
        {
            var email = Request.Form["email"][0];
            var birthdateToConvert = Request.Form["birthdate"][0];
            DateTime birthdate;
            var password = Request.Form["password"][0];
            var repeatpassword = Request.Form["repeatpassword"][0];

            if (email == null)
                return BadRequest();
            if (!DateTime.TryParse(birthdateToConvert, out birthdate))
                return BadRequest();
            if (password == null)
                return BadRequest();
            if (repeatpassword == null)
                return BadRequest();
            if (password != repeatpassword)
                return BadRequest();

            var user = new ApplicationUser()
            {
                Birthdate = birthdate,
                Email = email,
                UserName = email
            };

            if (user == null)
                return BadRequest();

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (_context.Users.Count() == 1)
                    await _userManager.AddToRoleAsync(user, "Admin");
                else
                    await _userManager.AddToRoleAsync(user, "User");
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
                return BadRequest();
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