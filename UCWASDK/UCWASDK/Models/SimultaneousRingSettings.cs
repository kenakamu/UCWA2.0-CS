using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
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

        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }

        public Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task SimultaneousRingToContact()
        {
            return SimultaneousRingToContact(HttpService.GetNewCancellationToken());
        }

        public Task SimultaneousRingToContact(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.simultaneousRingToContact, this, cancellationToken);
        }

        public Task SimultaneousRingToDelegates()
        {
            return SimultaneousRingToDelegates(HttpService.GetNewCancellationToken());
        }

        public Task SimultaneousRingToDelegates(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.simultaneousRingToDelegates, this, cancellationToken);
        }

        public Task SimultaneousRingToTeam(int? ringDelay)
        {
            return SimultaneousRingToTeam(ringDelay, HttpService.GetNewCancellationToken());
        }

        public Task SimultaneousRingToTeam(int? ringDelay, CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.simultaneousRingToTeam, this, cancellationToken);
        }
    }
}
