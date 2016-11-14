using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an error transcript within a conversationLog. 
    /// </summary>
    public class ErrorTranscript : UCWAModelBaseLink
    {
        [JsonProperty("reason")]
        public ErrorTranscriptReason Reason { get; internal set; }
    }
}
