using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
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

        public async Task<OnlineMeetingExtension> Create(OnlineMeetingExtension onlineMeetingExtension)
        {
            return await HttpService.Post<OnlineMeetingExtension>(Self, onlineMeetingExtension);
        }        
    }
}
