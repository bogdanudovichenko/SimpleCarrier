using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SimpleCarrier.API.Options
{
    public class AuthOptions
    {
        public const string Issuer = "SimpleCarrierAuthServer";
        public static SecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes("my_super_secret_key"));
    }
}