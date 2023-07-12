namespace Bookshelf.Core.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmail(List<string> receivers, string body, string subject, string smtp, string email, string password, int port);
    }
}
