using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents whether a participant is using the audio modality in a conversation. 
    /// This resource helps the application track when a participant joins or leaves this modality. 
    /// </summary>
    public class ParticipantAudio : UCWAModelBase
    {
        [JsonProperty("audioDirection")]
        public MediaDirectionType AudioDirection { get; internal set; }

        [JsonProperty("audioMuted")]
        public bool AudioMuted { get; internal set; }

        [JsonProperty("audioSourceId")]
        public string AudioSourceId { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("muteAudio")]
            internal MuteAudio muteAudio { get; set; }
  
            [JsonProperty("participant")]
            internal UCWAHref participant { get; set; }
  
            [JsonProperty("unmuteAudio")]
            internal UnmuteAudio unmuteAudio { get; set; }
        }

        public Task MuteAudio()
        {
            return MuteAudio(HttpService.GetNewCancellationToken());
        }
        public Task MuteAudio(CancellationToken cancellationToken)
        {
             return HttpService.Post(Links.muteAudio, "", cancellationToken);
        }

        public Task<Participant> GetParticipant()
        {
            return GetParticipant(HttpService.GetNewCancellationToken());
        }
        public Task<Participant> GetParticipant(CancellationToken cancellationToken)
        {
            return  HttpService.Get<Participant>(Links.participant, cancellationToken);
        }

        public Task UnmuteAudio()
        {
            return UnmuteAudio(HttpService.GetNewCancellationToken());
        }
        public Task UnmuteAudio(CancellationToken cancellationToken)
        {
             return HttpService.Post(Links.unmuteAudio, "", cancellationToken);
        }
    }
}
