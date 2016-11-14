using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a user's settings to simultaneously send incoming calls to a specified target.
    /// The user's incoming calls can be simultaneously sent to a contact, delegates, or team, as well as to the user. 
    /// </summary>
    public class SimultaneousRingSettings : UCWAModelBase
    {
        [JsonProperty("ringDelay")]
        public int RingDelay { get; set; }

        [JsonProperty("target")]
        public SimultaneousRingTarget Target { get; set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref contact { get; set; }

            [JsonProperty("simultaneousRingToContact")]
            internal SimultaneousRingToContact simultaneousRingToContact { get; set; }

            [JsonProperty("simultaneousRingToDelegates")]
            internal SimultaneousRingToDelegates simultaneousRingToDelegates { get; set; }

            [JsonProperty("simultaneousRingToTeam")]
            internal SimultaneousRingToTeam simultaneousRingToTeam { get; set; }
        }
                
        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task SimultaneousRingToContact()
        {
            await HttpService.Post(Links.simultaneousRingToContact, this);
        }

        public async Task SimultaneousRingToDelegates()
        {
            await HttpService.Post(Links.simultaneousRingToDelegates, this);
        }

        public async Task SimultaneousRingToTeam(int? ringDelay)
        {
            await HttpService.Post(Links.simultaneousRingToTeam, this);
        }
    }
}
