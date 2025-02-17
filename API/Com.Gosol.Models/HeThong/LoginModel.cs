namespace Com.Gosol.VHTT.Models.HeThong
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = "";
        public string Ticket { get; set; } = "";
        public string Captcha { get; set; }
    }
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
