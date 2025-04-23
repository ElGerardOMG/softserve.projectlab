using API.implementations.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Models.Entities;
using System.Text.Json;
using System.Net;
using API.Models.UserData;
using API.Utils.Implementations;

namespace API.Controllers;

[Route("address")]
[ApiController]
[Authorize]
public class UserAddressesController : ControllerBase
{
    private readonly IUserAddressProcessor _IUserAddressProcessor;

    public UserAddressesController(IUserAddressProcessor userAddressProcessor)
    {
        _IUserAddressProcessor = userAddressProcessor;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAddresses()
    {
        var result = DetermineErrorCode(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );

        if (!(result is OkResult)) return result;
        
        List<UserAddress> userAddresses = _IUserAddressProcessor.GetAddressesFromUser(userId);
        String json = JsonSerializer.Serialize(userAddresses);
        Console.WriteLine(json);
        return Ok(json);
    }

    [HttpPut]
    public async Task<IActionResult> AddNewAddress([FromBody] UserAddressDTO userAddress)
    {
        var result = DetermineErrorCode(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );

        if (!(result is OkResult)) return result;


        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> EditAddress([FromBody] UserAddressDTO userAddress)
    {
        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAddress([FromBody] UserAddressDTO userAddress)
    {
        return Ok();
    }

    [NonAction]
    private IActionResult DetermineErrorCode(int errorCode)
    {
        
        switch (errorCode)
        {
            case UserClaimUtils.SUCCESFUL: return Ok();
            case UserClaimUtils.UNAUTHORIZED: return Unauthorized();
            case UserClaimUtils.INVALID_TOKEN: return StatusCode(500, "Invalid Session Token. Plase try signing in again");
            case UserClaimUtils.INVALID_PARSING_TOKEN: return StatusCode(500, "Invalid Session Token. Plase try signing in again");
            default: return Ok();
        }
    }

}
