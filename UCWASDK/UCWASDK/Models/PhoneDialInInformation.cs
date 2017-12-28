using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents phone access information for an onlineMeeting.
    /// </summary>
    public class PhoneDialInInformation : UCWAModelBaseLink
    {
        [JsonProperty("conferenceId")]
        public string ConferenceId { get; internal set; }

        [JsonProperty("defaultRegion")]
        public string DefaultRegion { get; internal set; }

        [JsonProperty("externalDirectoryUri")]
        public string ExternalDirectoryUri { get; internal set; }

        [JsonProperty("internalDirectoryUri")]
        public string InternalDirectoryUri { get; internal set; }

        [JsonProperty("isAudioConferenceProviderEnabled")]
        public bool IsAudioConferenceProviderEnabled { get; internal set; }

        [JsonProperty("participantPassCode")]
        public string ParticipantPassCode { get; internal set; }

        [JsonProperty("tollFreeNumbers")]
        public string[] TollFreeNumbers { get; internal set; }

        [JsonProperty("tollNumber")]
        public string TollNumber { get; internal set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public DialInRegion[] DialInRegion { get { return Embedded?.dialInRegion; } }

        internal class InternalEmbedded
        {
            [JsonProperty("dialInRegion")]
            internal DialInRegion[] dialInRegion { get; set; }               
        }        
    }
}
