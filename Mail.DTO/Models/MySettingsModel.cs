namespace Mail.DTO.Models
{
    public class MySettingsModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }//
        public string SmtpClient { get; set; }//
        public int Port { get; set; }
        public int KeyWithPercentDispatchExecution { get; set; }
        public int KeyWithWholeDispatchExecution { get; set; }
        public int KeyWithPercentageCompletion { get; set; }
    }
}
