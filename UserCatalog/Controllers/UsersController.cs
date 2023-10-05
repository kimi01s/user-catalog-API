using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserCatalog.Data.repositories;
using UserCatalog.Model;
using System;
using System.Text;

namespace UserCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetAllUsers() { 
            return Ok(await _userRepo.GetAllUsers());
        }

        [HttpPost]
        [Route("NewUser")]
        public async Task<IActionResult> InsertUser(UsersModel User) { 
            if (User == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
            }
            var pswd = Encoding.UTF8.GetBytes(User.Password);
            var base64 = Convert.ToBase64String(pswd);
            User.Password = base64;

            var created = await _userRepo.InsertUser(User);
            return Created("created",created);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, UsersModel user)
        {
            if (User == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userRepo.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(UsersModel user) { 

            await _userRepo.DeleteUser(user.Id);
            return NoContent();
         }
    }
}
