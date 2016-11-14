using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents access information for phone users who wish to join an onlineMeeting. 
    /// </summary>
    public class DialInRegion : UCWAModelBaseLink
    {
        [JsonProperty("languages")]
        public string[] Languages { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("number")]
        public string Number { get; internal set; }
    }
}
