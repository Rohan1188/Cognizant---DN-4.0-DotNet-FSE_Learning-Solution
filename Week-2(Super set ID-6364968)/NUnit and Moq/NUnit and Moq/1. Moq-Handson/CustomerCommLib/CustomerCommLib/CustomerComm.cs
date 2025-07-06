namespace CustomerCommLib
{
    public class CustomerComm
    {
        private IMailSender _mailSender;

        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public bool SendMailToCustomer()
        {
            string to = "cust123@abc.com";
            string message = "Some Message";
            _mailSender.SendMail(to, message);
            return true;
        }
    }
}
