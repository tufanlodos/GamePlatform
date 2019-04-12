using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GamePlatform.Web.Api.Security {
    public static class KeyProvider {
        public static string SecurityKey {
            get {
                return "this_is_the_security_key_we_need";
            }
        }

        public static SymmetricSecurityKey SymmetricSecurityKey {
            get {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            }
        }
    }
}
