using EscNet.Mails.Interfaces;
using EscNet.Mails.Models;
using Grpc.Core;

namespace MailgRPC.Server.Services
{
    public class MailService : Mailing.MailingBase
    {
        private readonly IEmailSender _emailSender;

        public MailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override async Task<MailResponse> SendMail(MailRequest request, ServerCallContext context)
        {
            try
            {
                var email = new Email
                {
                    Receiver = request.Receiver,
                    Subject = request.Subject,
                    Body = request.Body,
                };

                var success = await _emailSender.SendEmailAsync(email);

                if (success)
                    return new MailResponse
                    {
                        Message = "Mail sended with success!",
                        Success = true,
                    };
            }
            catch (Exception error)
            {
                return new MailResponse
                {
                    Message = error.Message,
                    Success = false,
                };
            }

            return new MailResponse
            {
                Message = "Mail not sent!",
                Success = false,
            };
        }
    }
}
