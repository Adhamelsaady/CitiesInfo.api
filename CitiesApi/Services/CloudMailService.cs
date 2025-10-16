namespace CitiesApi.Services
{
    public class CloudMailService : IMailService   
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:_mailTo"];
            _mailFrom = configuration["mailSettings:_mailFrom"];
        }
        public void Send(string subject, string message)
        {
            // send mail - output to console window
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
