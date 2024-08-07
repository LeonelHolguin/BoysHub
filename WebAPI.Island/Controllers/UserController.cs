using Island.Core.Application.Interfaces.Services;
using Island.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Island.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserViewModel userToRegister)
        {
            UserViewModel userCreated = new();

            try
            {
                bool userNameIsTaken = await _userService.UserNameExists(userToRegister.UserName!);

                if (userNameIsTaken)
                    return BadRequest("Username is alredy taked");

                userCreated = await _userService.Register(userToRegister);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("", userCreated);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginSuccessViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginUser)
        {
            LoginSuccessViewModel userLoginSuccess = new();

            try
            {
                userLoginSuccess = await _userService.Login(loginUser);

                if (userLoginSuccess.AccessToken is null)
                    return NotFound("Incorrect username or password");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(userLoginSuccess);
        }

        [HttpPut]
        [Route("edit")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UserViewModel userToUpdate)
        {
            try
            {
                await _userService.Update(userToUpdate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{userId}/delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await _userService.Delete(userId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
