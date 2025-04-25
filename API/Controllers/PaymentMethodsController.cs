using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

using API.DTOs.PaymentMethodsDTO;
using API.Utils.Implementations;
using API.implementations.Domain;
using API.Models;


namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PaymentMethodsController : ControllerBase
{
    private readonly UserPaypalProcessor _UserPaypalProcessor;
    private readonly UserCardProcessor _UserCardProcessor;

    public PaymentMethodsController(UserPaypalProcessor paypal, UserCardProcessor card)
    {
        _UserPaypalProcessor = paypal;
        _UserCardProcessor = card;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserPaymentMethods()
    {
        var result = this.GenerateResultFromIdentity(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );
        if (!(result is OkResult)) return result;


        List<object> paymentMethods = new List<object>();


        foreach (UserCardPayment card in _UserCardProcessor.GetMethodsFromUser(userId))
        {
            CardInfoReadDTO cardDTO = DtoMapper.Mapper<UserCardPayment, CardInfoReadDTO>(card, true);

            paymentMethods.Add(new { type = "Card", data = cardDTO });

        }

        foreach (UserPaypalPayment paypal in _UserPaypalProcessor.GetMethodsFromUser(userId))
        {
            PaypalInfoDTO paypalDTO = DtoMapper.Mapper<UserPaypalPayment, PaypalInfoDTO>(paypal, true);

            paymentMethods.Add(new {type = "Paypal", data = paypalDTO });

        }
        return new JsonResult(paymentMethods);
    }

    [HttpPut("card")]
    public async Task<IActionResult> AddNewCard([FromBody] CardInfoWriteDTO cardDTO)
    {
        var result = this.GenerateResultFromIdentity(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );

        if (!(result is OkResult)) return result;

        UserCardPayment card = DtoMapper.Mapper<CardInfoWriteDTO, UserCardPayment>(cardDTO, true);

        card.UserId = userId;

        if (_UserCardProcessor.AddMethodToUser(card))
        {
            return Ok("Card method added succesfully");
        }

        return StatusCode(500, "Couldn't add new card");
    }

    [HttpPatch("card/{cardId}")]
    public async Task<IActionResult> EditCardMethod([FromRoute] int cardId, [FromBody] CardInfoWriteDTO cardDTO)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserCardPayment card = (UserCardPayment)_UserCardProcessor.GetMethodById(userId);
            if (card == null)
            {
                return NotFound("This payment method doesn't exist");
            }
            if (card.UserId != userId)
            {
                return Unauthorized("You have no permission to perform this action!");
            }

            DtoMapper.Updater(cardDTO, card, null, 0);

            if (_UserCardProcessor.UpdateMethod(card))
            {
                return Ok("User's card updated succesfully");
            }

            return StatusCode(500, "Couldn't update method");
            

        }
        catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }
    }


    [HttpDelete("card/{cardId}")]
    public async Task<IActionResult> DeleteCard([FromRoute] int cardId)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserCardPayment card = (UserCardPayment) _UserCardProcessor.GetMethodById(cardId);
            if (card == null)
            {
                return NotFound("This payment method doesn't exist");
            }

            if (card.UserId != userId)
            {
                return Unauthorized("You have no permission to perform this action!");
            }

            if(_UserCardProcessor.RemoveMethod(cardId) != null)
            {
                return Ok("User's card removed succesfully");
            }
            
            return StatusCode(500, "Couldn't delete method");
            
        }
        catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }

    }

    [HttpPut("paypal")]
    public async Task<IActionResult> AddNewPaypal([FromBody] PaypalInfoDTO paypalDTO)
    {
        var result = this.GenerateResultFromIdentity(
            UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
        );

        if (!(result is OkResult)) return result;

        UserPaypalPayment paypal = DtoMapper.Mapper<PaypalInfoDTO, UserPaypalPayment>(paypalDTO, true);

        paypal.UserId = userId;

        if (_UserPaypalProcessor.AddMethodToUser(paypal))
        {
            return Ok("Paypal method added succesfully");
        }

        return StatusCode(500,"Couldn't add new paypal method");

    }

    [HttpPatch("paypal/{paypalId}")]
    public async Task<IActionResult> EditPaypalMethod([FromRoute] int paypalId, [FromBody] PaypalInfoDTO paypalDTO)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserPaypalPayment paypal = (UserPaypalPayment) _UserPaypalProcessor.GetMethodById(paypalId);
            if(paypal == null)
            {
                return NotFound("This payment method doesn't exist");
            }
            if (paypal.UserId != userId)
            {
                return Unauthorized("You have no permission to perform this action!");
            }

            DtoMapper.Updater(paypalDTO, paypal, null, 0);

            if (_UserPaypalProcessor.UpdateMethod(paypal))
            {
                return Ok("Paypal method updated succesfully");
            }

            return StatusCode(500, "Couldn't edit paypal method");

        }
        catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }
    }


    [HttpDelete("paypal/{paypalId}")]
    public async Task<IActionResult> DeletePaypalMethod([FromRoute] int paypalId)
    {
        try
        {
            var result = this.GenerateResultFromIdentity(
                UserClaimUtils.GetIdFromIdentity(User.Identity as ClaimsIdentity, out int userId)
            );

            if (!(result is OkResult)) return result;

            UserPaypalPayment paypal = (UserPaypalPayment) _UserPaypalProcessor.GetMethodById(paypalId);
            if (paypal == null)
            {
                return NotFound("This payment method doesn't exist");
            }
            if (paypal.UserId != userId)
            {
                return Unauthorized("You have no permission to perform this action!");
            }

            if (_UserPaypalProcessor.RemoveMethod(paypal) != null)
            {
                return Ok("Paypal deleted succesfully");
            }
            

            return StatusCode(500,"Couldn't delete paypal");
        }
        catch (Exception E)
        {
            Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
            return StatusCode(500, "Something went wrong. Please try again");
        }

    }

}
