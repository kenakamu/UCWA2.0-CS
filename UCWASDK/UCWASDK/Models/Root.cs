using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the entry point to the API. 
    /// The root resource lets the application choose the kind of credentials it is going to present. Currently, only user credentials are supported. The root resource also informs the application about the location of the cross-domain frame ( xframe) needed for web programming. 
    /// </summary>
    public class Root : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("redirect")]
            internal UCWAHref redirect { get; set; }
       
            [JsonProperty("user")]
            internal UCWAHref user { get; set; }
       
            [JsonProperty("xframe")]
            internal UCWAHref xframe { get; set; }
        }

        public async Task<Redirect> GetRedirect()
        {
            return Links.redirect == null ? null : await HttpService.Get<Redirect>(Links.redirect);
        }

        public async Task<User> GetUser()
        {
            return await HttpService.Get<User>(Links.user);
        }

        public async Task<Xframe> GetXframe()
        {
            return await HttpService.Get<Xframe>(Links.xframe);
        }
    }
}
