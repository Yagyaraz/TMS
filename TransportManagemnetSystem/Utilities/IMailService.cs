using TransportManagemnetSystem.Models;

namespace TransportManagemnetSystem.Utilities
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
