using YCompanyCommunicationsAPI.Models;

namespace YCompanyCommunicationsAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
