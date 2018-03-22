using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestClient
{
    public class TokenService
    {
        public const string tenant = ""; // i.e. "contoso.onmicrosoft.com";
        public static string username = ""; // i.e. "user1@contoso.onmicrosoft.com";
        public static string password = ""; // i.e. "password"
        private static string clientId = ""; // i.e. "4611b759-70fb-4465-953f-17949b15490a";
        private static string sfbResourceId = "00000004-0000-0ff1-ce00-000000000000";
        private static string aadInstance = "https://login.microsoftonline.com/{0}";

        static public string AquireAADToken(string resourceId = "")
        {
            if (string.IsNullOrEmpty(resourceId))
                resourceId = sfbResourceId;
            else
                resourceId = new Uri(resourceId).Scheme + "://" + new Uri(resourceId).Host;
           
            var authContext = new AuthenticationContext(string.Format(aadInstance, tenant));
            
            var cred = new UserPasswordCredential(username, password);
            AuthenticationResult authenticationResult = null;

            try
            {
                authenticationResult = authContext.AcquireTokenAsync(resourceId, clientId, cred).Result;
            }
            catch (Exception ex)
            {
            }

            return authenticationResult.AccessToken;
        }

        static public string AquireOnPremToken(string resourceId = "")
        {
            // You may want to consider cache the token as it only expired in 8 hours.
            // https://msdn.microsoft.com/en-us/skype/ucwa/authenticationinucwa
            using (HttpClient client = new HttpClient())
            {
                // Get OAuth service url
                var response = client.GetAsync(resourceId).Result;
                var wwwAuthenticate = response.Headers.WwwAuthenticate;
                var uri = wwwAuthenticate.First(x => x.Scheme == "MsRtcOAuth").Parameter.Split(',').First(y => y.Contains("href")).Split('=')[1].Replace("\"", "");

                // Obtain AccessToken
                response = client.PostAsync(uri, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("grant_type", "password"), new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password) })).Result;
                return JToken.Parse(response.Content.ReadAsStringAsync().Result)["access_token"].ToString();
            }
        }

        static public void SignOut()
        {
            var authContext = new AuthenticationContext(string.Format(aadInstance, tenant));
            authContext.TokenCache.Clear();
        }
    }
}
