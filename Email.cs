namespace SendEmailMailKit
{
    public class Email
    {
        public Email(string fromDisplay, string from, string toEmailDisplay, string toEmail, string subject, string body)
        {
            FromDisplay = fromDisplay;
            From = from;
            ToEmailDisplay = toEmailDisplay;
            ToEmail = toEmail;
            Subject = subject;
            Body = body;
        }

        public string FromDisplay { get; set; }
        public string From { get; set; }
        public string ToEmailDisplay { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }       
}
