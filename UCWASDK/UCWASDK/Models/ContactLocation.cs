using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a contact's location. 
    /// contactLocation gets updated whenever the contact's location changes. 
    /// </summary>
    public class ContactLocation : UCWAModelBaseLink
    {
        [JsonProperty("location")]
        public string Location { get; internal set; }       
    }
}
