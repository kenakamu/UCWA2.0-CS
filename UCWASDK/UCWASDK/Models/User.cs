using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the entry point to the API using user credentials. 
    /// The user resource indicates that the application plans to use the API on behalf of a user. If an application attempts to use this resource without credentials, the server will respond with a 401 Not Authorized and authentication hints in the WWW-Authenticate Header. After credentials are acquired, this resource will point the application to the applications resource. In some cases, after credentials are acquired, the server might serve a redirect resource indicating that the user is homed on another server. The application should follow this resource to the new server. Credentials might need to be resubmitted.
    /// </summary>
    public class User : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("applications")]
            internal Applications2 applications { get; set; }

            [JsonProperty("redirect")]
            internal Redirect redirect { get; set; }

            [JsonProperty("xframe")]
            internal Xframe xframe { get; set; }
        }

        public async Task<Application> CreateApplication(string userAgent, string EndpointId, string Culture)
        {
            Application application = new Models.Application()
            {
                UserAgent = userAgent,
                EndpointId = EndpointId,
                Culture = Culture
            };
            return await HttpService.Post<Application>(Links.applications, application);
        }

        public async Task<Redirect> GetRedirect()
        {
            return await HttpService.Get<Redirect>(Links.redirect);
        }

        public async Task<Xframe> GetXframe()
        {
            return await HttpService.Get<Xframe>(Links.xframe);
        }
    }
}
