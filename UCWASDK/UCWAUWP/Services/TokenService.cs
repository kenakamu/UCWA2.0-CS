using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCWAUWP
{
    public class TokenService
    {
        public const string tenant = ""; // i.e. "contoso.onmicrosoft.com";
        public static string username = ""; // i.e. "user1@contoso.onmicrosoft.com";
        public static string password = ""; // i.e. "password"
        private static string clientId = ""; // i.e. "4611b759-70fb-4465-953f-17949b15490a";
        private static string sfbResourceId = "00000004-0000-0ff1-ce00-000000000000";
        private static string aadInstance = "https://login.microsoftonline.com/{0}";
        private static string redirectUri = ""; // i.e. "http://localhost/ucwasample";

        static public async Task<string> AquireAADToken(string resourceId = "")
        {
            if (string.IsNullOrEmpty(resourceId))
                resourceId = sfbResourceId;
            else
                resourceId = new Uri(resourceId).Scheme + "://" + new Uri(resourceId).Host;
           
            var authContext = new AuthenticationContext(string.Format(aadInstance, tenant));
            
            AuthenticationResult authenticationResult = null;

            try
            {
                authenticationResult = await authContext.AcquireTokenAsync(
                    resourceId, clientId, new Uri(redirectUri), new PlatformParameters(PromptBehavior.Auto, false));
            }
            catch (Exception ex)
            {
            }

            return authenticationResult.AccessToken;
        }        

        static public void SignOut()
        {
            var authContext = new AuthenticationContext(string.Format(aadInstance, tenant));
            authContext.TokenCache.Clear();
        }
    }
}
