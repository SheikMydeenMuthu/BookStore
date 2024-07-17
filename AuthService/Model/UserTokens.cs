using System;
namespace AuthService.Model
{
    public class UserTokens
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public string EmailId { get; set; }
        public Guid GuidId { get; set; }
    }
}

