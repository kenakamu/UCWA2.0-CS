using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the communication modalities supported by a contact. 
    /// This resource gives the set of modalities currently supported by a contact. If an application has subscribed to a contact, events will be raised when a contact's modalities change. 
    /// </summary>
    public class ContactSupportedModalities : UCWAModelBaseLink
    {
        [JsonProperty("modalities")]
        public ConversationModalityType[] Modalities { get; internal set; }       
    }
}
