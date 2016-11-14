using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
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

        public async Task MuteAudio()
        {
            await HttpService.Post(Links.muteAudio, "");
        }

        public async Task<Participant> GetParticipant()
        {
            return await HttpService.Get<Participant>(Links.participant);
        }

        public async Task UnmuteAudio()
        {
            await HttpService.Post(Links.unmuteAudio, "");
        }
    }
}
