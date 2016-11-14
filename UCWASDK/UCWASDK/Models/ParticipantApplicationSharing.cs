using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents whether a participant is using the application sharing modality in a conversation. 
    /// This resource helps the application track when a participant joins or leaves this modality. 
    /// </summary>
    public class ParticipantApplicationSharing : UCWAModelBase
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
