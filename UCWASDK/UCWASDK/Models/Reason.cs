using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Error response. This is never explicitly requested by the client - but the client will receive this in the event of an error. 
    /// </summary>
    public class Reason : UCWAModelBaseLink
    {
        [JsonProperty("code")]
        public ErrorCode Code { get; internal set; }

        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("subcode")]
        public ErrorSubCode Subcode { get; internal set; }
    }
}
