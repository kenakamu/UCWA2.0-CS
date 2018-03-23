using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents whether a participant is using the panoramic video modality in a conversation. 
    /// This resource helps the application track when a participant joins or leaves this modality. 
    /// </summary>
    public class ParticipantPanoramicVideo : UCWAModelBase
    {

        [JsonProperty("panoramicVideoDirection")]
        public MediaDirectionType PanoramicVideoDirection { get; internal set; }

        [JsonProperty("panoramicVideoMuted")]
        public bool PanoramicVideoMuted { get; internal set; }

        [JsonProperty("panoramicVideoSourceId")]
        public string PanoramicVideoSourceId { get; internal set; }

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
