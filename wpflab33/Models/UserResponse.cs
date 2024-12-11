using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpflab33.Models
{
    internal class UserResponse
    {
        public string? access_token { get; set; }
        public string? username { get; set; }

        public UserResponse(string? access_token, string? username)
        {
            this.access_token = access_token;
            this.username = username;
        }
    }
}
