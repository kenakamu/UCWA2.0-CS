using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
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

        public Task<Participant> GetParticipant()
        {
            return GetParticipant(HttpService.GetNewCancellationToken());
        }
        public Task<Participant> GetParticipant(CancellationToken cancellationToken)
        {
            return HttpService.Get<Participant>(Links.participant, cancellationToken);
        }
    }
}
