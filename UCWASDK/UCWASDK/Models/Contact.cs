using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a person or service that the user can communicate and collaborate with. 
    /// contact is the persistent representation of a person or service. A contact can be referenced by a participant or in the context of communication. A contact resource can also be referenced by various containers such as a group or subscribedContacts. 
    /// </summary>
    public class Contact : UCWAModelBase
    {
        [JsonProperty("company")]
        public string Company { get; internal set; }

        [JsonProperty("department")]
        public string Department { get; internal set; }

        [JsonProperty("emailAddresses")]
        public string[] EmailAddresses { get; internal set; }

        [JsonProperty("homePhoneNumber")]
        public string HomePhoneNumber { get; internal set; }

        [JsonProperty("sourceNetworkIconUrl")]
        public string SourceNetworkIconUrl { get; internal set; }

        [JsonProperty("mobilePhoneNumber")]
        public string MobilePhoneNumber { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("office")]
        public string Office { get; internal set; }

        [JsonProperty("otherPhoneNumber")]
        public string OtherPhoneNumber { get; internal set; }

        [JsonProperty("sourceNetwork")]
        public string SourceNetwork { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("type")]
        public ContactType Type { get; internal set; }

        [JsonProperty("uri")]
        public string Uri { get; internal set; }

        [JsonProperty("workPhoneNumber")]
        public string WorkPhoneNumber { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contactLocation")]
            internal UCWAHref contactLocation { get; set; }
 
            [JsonProperty("contactNote")]
            internal UCWAHref contactNote { get; set; }
 
            [JsonProperty("contactPhoto")]
            internal UCWAHref contactPhoto { get; set; }
 
            [JsonProperty("contactPresence")]
            internal UCWAHref contactPresence { get; set; }
 
            [JsonProperty("contactPrivacyRelationship")]
            internal UCWAHref contactPrivacyRelationship { get; set; }
 
            [JsonProperty("contactSupportedModalities")]
            internal UCWAHref contactSupportedModalities { get; set; }
        }

        public Task<ContactLocation> GetContactLocation()
        {
            return GetContactLocation(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactLocation> GetContactLocation(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactLocation>(Links.contactLocation, cancellationToken);
        }

        public Task<ContactNote> GetContactNote()
        {
            return GetContactNote(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactNote> GetContactNote(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactNote>(Links.contactNote, cancellationToken);
        }

        public Task<byte[]> GetContactPhoto()
        {
            return GetContactPhoto(HttpService.GetNewCancellationToken());
        }
        public async Task<byte[]> GetContactPhoto(CancellationToken cancellationToken)
        {
            return await HttpService.GetBinary(Links.contactPhoto, cancellationToken);
        }

        public Task<ContactPresence> GetContactPresence()
        {
            return GetContactPresence(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactPresence> GetContactPresence(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactPresence>(Links.contactPresence, cancellationToken);
        }

        public Task<ContactPrivacyRelationship> GetContactPrivacyRelationship()
        {
            return GetContactPrivacyRelationship(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactPrivacyRelationship> GetContactPrivacyRelationship(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactPrivacyRelationship>(Links.contactPrivacyRelationship, cancellationToken);
        }

        public Task<ContactSupportedModalities> GetContactSupportedModalities()
        {
            return GetContactSupportedModalities(HttpService.GetNewCancellationToken());
        }
        public async Task<ContactSupportedModalities> GetContactSupportedModalities(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ContactSupportedModalities>(Links.contactSupportedModalities, cancellationToken);
        }
    }
}
