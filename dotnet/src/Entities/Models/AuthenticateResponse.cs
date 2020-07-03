using System;

namespace Entities.Models {
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            //FirstName = user.FirstName;
            //LastName = user.LastName;
            Token = token;
        }
    }
}