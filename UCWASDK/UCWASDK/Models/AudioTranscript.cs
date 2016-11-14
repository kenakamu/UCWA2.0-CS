using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an audio transcript within a conversationLog. 
    /// </summary>
    public class AudioTranscript : UCWAModelBaseLink
    {
        [JsonProperty("duration")]
        public string Duration { get; internal set; }

        [JsonProperty("status")]
        public string Status { get; internal set; }
    }
}
