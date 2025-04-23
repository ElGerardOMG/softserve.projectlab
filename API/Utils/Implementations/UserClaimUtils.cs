using System.Security.Claims;
using API.Models.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Utils.Implementations;
// TODO: Maybe implementing a custom mapper class that allows to customize property mapping 
public static class UserClaimUtils
{
    public const int SUCCESFUL = 0;
    public const int UNAUTHORIZED = 1;
    public const int INVALID_TOKEN = 2;
    public const int INVALID_PARSING_TOKEN = 3;
    public static ClaimsIdentity CreateIdentity(User user)
    {
        return new ClaimsIdentity(new List<Claim>(){

            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),

         });
    }

    public static int GetIdFromIdentity(ClaimsIdentity identity, out int userId)
    {
        userId = -1;
        if (identity == null || !identity.IsAuthenticated)
        {
            return UNAUTHORIZED;
        }


        var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {

            return INVALID_TOKEN;

        }

        // Convertir a int
        if (!int.TryParse(userIdClaim.Value, out int id))
        {
            return INVALID_PARSING_TOKEN;
        }

        userId = id;

        return 0;
    }

    public static IActionResult GenerateResultFromIdentity(this ControllerBase controller, int errorCode)
    {
        switch (errorCode)
        {
            case UserClaimUtils.SUCCESFUL: 
                return controller.Ok();
            case UserClaimUtils.UNAUTHORIZED: 
                return controller.Unauthorized("You must be logged in to perform this action");
            case UserClaimUtils.INVALID_TOKEN: 
                return controller.StatusCode(500, "Invalid Session Token. Plase try signing in again");
            case UserClaimUtils.INVALID_PARSING_TOKEN: 
                return controller.StatusCode(500, "Invalid Session Token. Plase try signing in again");
            default: 
                return controller.Ok();
        }
    }

}
