using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the privacy relationship between the user and a contact. 
    /// This resource captures the closeness of the relationship, with more information available in closer relationships. FriendsAndFamily contacts see appointment and meeting titles, while Colleagues see only Free or Busy. If an application has subscribed to a contact, events will be raised when a contact's privacy relationship changes. 
    /// </summary>
    public class ContactPrivacyRelationship : UCWAModelBaseLink
    {
        [JsonProperty("relationshipLevel")]
        public PrivacyRelationshipLevel RelationshipLevel { get; internal set; }       
    }
}
