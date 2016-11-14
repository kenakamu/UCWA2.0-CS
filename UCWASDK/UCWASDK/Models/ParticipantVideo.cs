using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents whether a participant is using the main video modality in a conversation. 
    /// This resource helps the application track when a participant joins or leaves this modality. 
    /// </summary>
    public class ParticipantVideo : UCWAModelBase
    {
        [JsonProperty("videoDirection")]
        public MediaDirectionType VideoDirection { get; internal set; }

        [JsonProperty("videoMuted")]
        public bool VideoMuted { get; internal set; }

        [JsonProperty("videoSourceId")]
        public string VideoSourceId { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("muteVideo")]
            internal MuteVideo muteVideo { get; set; }
   
            [JsonProperty("participant")]
            internal UCWAHref participant { get; set; }
   
            [JsonProperty("unmuteVideo")]
            internal UnmuteVideo unmuteVideo { get; set; }
        }

        public async Task MuteVideo()
        {
            await HttpService.Post(Links.muteVideo, "");
        }

        public async Task<Participant> GetParticipant()
        {
            return await HttpService.Get<Participant>(Links.participant);
        }

        public async Task UnmuteVideo()
        {
            await HttpService.Post(Links.unmuteVideo, "");
        }
    }
}
