using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Used to determine whether a participant is using the instant messaging modality in a conversation. 
    /// The application can use this resource to monitor when a participant joins or leaves the messaging modality.
    /// </summary>
    public class ParticipantMessaging : UCWAModelBase
    {      
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("participant")]
            internal UCWAHref participant { get; set; }
        }

        public async Task<Participant> GetParticipant()
        {
            return await HttpService.Get<Participant>(Links.participant);
        }
    }
}
