using API.implementations.Domain;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.implementations;
using System.Net;
using Microsoft.Data.SqlClient;



namespace API.Controllers
{

    /* Test Class made only for learning and testing purposes. Must be deleted after. */

    [ApiController]
    [Route("api/users")]
    public class TestSignUpController : ControllerBase
    {

        private readonly ILogger<TestSignUpController> _logger;

        public TestSignUpController(ILogger<TestSignUpController> logger)
        {
            _logger = logger;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp([FromBody] User user)
        {
            BDConnection bdc = new BDConnection("localhost", "12345", "app", "store", 1433);
            await bdc.Connect();
            try
            {

                var sql = "INSERT INTO users(email, pass) VALUES (@Email, @Password);";

                await using var command = new SqlCommand(sql, bdc.Connection);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException e)
            {
                _logger.LogError($"SQL Error: {e.Message}");
                return BadRequest("Error.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest("Error.");
            }

            return Ok("User registered");

        }

        public class User
        {
            //public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
    
}
