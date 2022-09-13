using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApp.BO;
using TestApp.DataContext;
using TestApp.DTO;
using TestApp.ServiceContracts;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<UserDetails> userList = _userServices.GetAllUser();
                if (userList == null)
                {
                    userList = new List<UserDetails>();
                }
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("users")]
        public IActionResult Post(UserDetailsDTO user)
        {
            try
            {
                if (string.IsNullOrEmpty(_userServices.GetUserByName(user.UserName)))
                {
                    int userId = _userServices.AddUser(user);
                    if (userId > 0)
                        return Ok(new { UserId = userId });
                    else
                        return BadRequest(new { Status = false,Message="User details not inserted into db" });
                }
                else
                {
                    return BadRequest(new { Status = true ,Message="Username already existing in db"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("users/{Id}")]
        public IActionResult Put(int Id, [FromBody] UserDetailsDTO user)
        {
            try
            {
                bool status = _userServices.UpdateUser(Id, user);
                if (status)
                {
                    return Ok(new { Status = true, Message = "User name updated" });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "User Id not found in db" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("users/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                bool status = _userServices.DeleteUser(Id);
                if (status)
                {
                    return Ok(new { Status = true, Message = "User details deleted" });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "User Id not found in db" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}