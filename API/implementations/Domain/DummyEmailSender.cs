using Microsoft.AspNetCore.Identity;
using API.Models.Entities;

namespace API.implementations.Domain
{
    /* Requerido por MapIdentityUser Por ahora no hace nada */
    public class DummyEmailSender : IEmailSender<User>
    {
        private readonly ILogger<DummyEmailSender> _logger;

        public DummyEmailSender(ILogger<DummyEmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            Console.WriteLine($"Tried to send email confirmation of {user.Id} to {email}");

        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            Console.WriteLine($"Tried to send password reset of {user.Id} to {email}");
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            Console.WriteLine($"Tried to send password reset link of {user.Id} to {email}");
        }
    }
}
