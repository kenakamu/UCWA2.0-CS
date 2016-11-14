using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// SDK original
    /// </summary>
    public class CallDecline : UCWAModelBaseLink
    {
        [JsonProperty("reason")]
        public CallDeclineReason Reason { get; internal set; }
    }
}
