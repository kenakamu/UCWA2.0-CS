using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
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

        public Task MuteVideo()
        {
            return MuteVideo(HttpService.GetNewCancellationToken());
        }
        public Task MuteVideo(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.muteVideo, "", cancellationToken);
        }

        public Task<Participant> GetParticipant()
        {
            return GetParticipant(HttpService.GetNewCancellationToken());
        }
        public Task<Participant> GetParticipant(CancellationToken cancellationToken)
        {
            return HttpService.Get<Participant>(Links.participant, cancellationToken);
        }

        public Task UnmuteVideo()
        {
            return UnmuteVideo(HttpService.GetNewCancellationToken());
        }
        public Task UnmuteVideo(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.unmuteVideo, "", cancellationToken);
        }
    }
}
