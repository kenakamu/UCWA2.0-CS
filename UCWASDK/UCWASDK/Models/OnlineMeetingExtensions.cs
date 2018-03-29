using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the set of onlineMeetingExtensions for the associated onlineMeeting. 
    /// </summary>
    public class OnlineMeetingExtensions : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public OnlineMeetingExtension[] MeetingExtensions { get { return Embedded.meetingExtensions; } }
        internal class InternalEmbedded
        {
            [JsonProperty("onlineMeetingExtension")]
            internal OnlineMeetingExtension[] meetingExtensions { get; set; }               
        }

        public Task<OnlineMeetingExtension> Create(OnlineMeetingExtension onlineMeetingExtension)
        {
            return Create(onlineMeetingExtension, HttpService.GetNewCancellationToken());
        }
        public Task<OnlineMeetingExtension> Create(OnlineMeetingExtension onlineMeetingExtension, CancellationToken cancellationToken)
        {
            return HttpService.Post<OnlineMeetingExtension>(Self, onlineMeetingExtension, cancellationToken);
        }        
    }
}
