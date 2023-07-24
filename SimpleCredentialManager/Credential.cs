using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class Credential
    {
        public Credential() { }

        public Credential(string username, string password, string email, string domain)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Domain = domain;
        }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
    }
}
