using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Models;
using System.Text.Json;
using API.Utils.Implementations;
using API.implementations.Domain.Interfaces;
using API.DTOs.UserData;

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
        var result = this.GenerateResultFromIdentity(
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
        var result = this.GenerateResultFromIdentity(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );

        if (!(result is OkResult)) return result;

        UserAddress userAddressNew = DtoMapper.Mapper<UserAddressDTO, UserAddress>(userAddress, true);

        userAddressNew.IdUser = userId;

        _IUserAddressProcessor.AddAddressToUser(userAddressNew);
        return Ok("Address added succesfully");
    }

    [HttpPatch("{address_id}")]
    public async Task<IActionResult> EditAddress([FromRoute] int address_id, [FromBody] UserAddressDTO userAddressDTO)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserAddress? userAddressEdit = _IUserAddressProcessor.GetAddressesFromUser(userId).FirstOrDefault(ua => ua.Id == address_id);

            if (userAddressEdit == null)
            {
                return BadRequest("This address doesn't exist!");
            }

            DtoMapper.Updater<UserAddressDTO, UserAddress>(userAddressDTO, userAddressEdit, ["Id"], 1);

            _IUserAddressProcessor.UpdateAddress(userAddressEdit);

            return Ok("User address updated succesfully");

        } catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }
    }


    [HttpDelete("{address_id}")]
    public async Task<IActionResult> DeleteAddress([FromRoute] int address_id)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserAddress? userAddressToRemove = _IUserAddressProcessor.GetAddressesFromUser(userId).FirstOrDefault(ua => ua.Id == address_id);

            if (userAddressToRemove == null)
            {
                return BadRequest("This address doesn't exist!");
            }

            _IUserAddressProcessor.RemoveAddress(userAddressToRemove);
        }
        catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }

        return Ok("User address removed succesfully");
    }


}
