using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using API.Utils.Implementations;
using System.Security.Claims;
using API.DTOs.UserData;

namespace API.Controllers;

[Route("user")]
[ApiController]

public class UserController : ControllerBase
{
    // Anteriormente, se inyectaba user processor, pero con la implementación de identiy, se usa userManager
    /*
     
    private readonly IUserProcessor _UserProcessor;
    
    public UserController(IUserProcessor userProcessor)
    {
        _UserProcessor = userProcessor;
    }
    */

    private readonly UserManager<User> _userManager;
    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    // Este requerería, dependiendo el contexto, algún tipo de autorización especial (¿Un rol específico? Ej: Admin)
    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public virtual IActionResult GetAll()
    {
        try
        {
            List<UserReadDTO> userDTOList = new List<UserReadDTO>();

            foreach (User u in (from u in _userManager.Users select u))
            {
                userDTOList.Add(DtoMapper.Mapper<User, UserReadDTO>(u, true));
            }

            return new JsonResult(userDTOList);

        }
        catch (Exception E)
        {
            Console.WriteLine(E.Message + "\n...\n" + E.StackTrace);
            return StatusCode(500, "Something went wrong, please try again");
        }


    }

    [HttpGet("{user_id}")]
    public async virtual Task<IActionResult> GetUser(int user_id)
    {
        try
        {
            User? user = _userManager.Users.FirstOrDefault(e => e.Id == user_id);

            if (user == null)
            {
                return NotFound("This user doesn't exist!");
            }

            bool newBool = user.IsActive.HasValue ? user.IsActive.Value : true;

            if (!newBool)
            {
                return NotFound("This user doesn't exist!");
            }

            UserReadDTO userDto = DtoMapper.Mapper<User, UserReadDTO>(user, true);

            return Ok(userDto);

        } catch (Exception E)
        {
            Console.WriteLine(E.Message + "\n...\n" + E.StackTrace);
            return StatusCode(500, "Something went wrong, please try again");
            
        }

    }

    /* This one must be protected... */
    [HttpDelete("{user_id}")]
    public virtual async Task<IActionResult> DeleteUser(int user_id)
    {
        try
        {
            User? user = await _userManager.FindByIdAsync(user_id+"");

            if (user == null)
            {
                return NotFound("This user doesn't exist!");
            }

            bool newBool = user.IsActive.HasValue ? user.IsActive.Value : true;

            if (!newBool)
            {
                return NotFound("This user doesn't exist!");
            }

            IdentityResult result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }

            return StatusCode(500, "Failed to delete user");

        }
        catch (Exception E)
        {
            Console.WriteLine(E.Message + "\n...\n" + E.StackTrace);
            return StatusCode(500, "Something went wrong, please try again");
        }

    }

    [Authorize]
    [HttpPatch]
    public virtual async Task<IActionResult> UpdateUser([FromBody] UserWriteDTO userDTO)
    {
        try
        {

            var resultCode = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int user_id)
            );
            if (!(resultCode is OkResult)) return resultCode;

            User? user = await _userManager.FindByIdAsync(user_id + "");

            if (user == null)
            {
                return NotFound("This user doesn't exist!");
            }

            bool newBool = user.IsActive.HasValue ? user.IsActive.Value : true;

            if (!newBool)
            {
                return NotFound("This user doesn't exist!");
            }
            
            DtoMapper.Updater(userDTO, user);
     
            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }

            return StatusCode(500, "Failed to update user");

        }
        catch (Exception E)
        {
            Console.WriteLine(E.Message + "\n...\n" + E.StackTrace);
            return StatusCode(500, "Something went wrong, please try again");
        }

    }



}



