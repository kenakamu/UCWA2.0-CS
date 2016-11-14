using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    public class Redirect : UCWAModelBase
    {   
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("user")]
            internal User user { get; set; }

            [JsonProperty("xframe")]
            internal Xframe xframe { get; set; }
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
