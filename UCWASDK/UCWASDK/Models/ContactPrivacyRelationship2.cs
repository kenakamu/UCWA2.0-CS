﻿using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the privacy relationship between the user and a contact. 
    /// This resource captures the closeness of the relationship, with more information available in closer relationships. FriendsAndFamily contacts see appointment and meeting titles, while Colleagues see only Free or Busy. If an application has subscribed to a contact, events will be raised when a contact's privacy relationship changes. 
    /// </summary>
    public class ContactPrivacyRelationship2 : UCWAModelBase
    {
        [JsonProperty("relationshipLevel")]
        public PrivacyRelationshipLevel RelationshipLevel { get; internal set; }

        [JsonProperty("_links")]
        internal new InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("resetContactPrivacyRelationship")]
            internal UCWAHref resetContactPrivacyRelationship { get; set; }
        }

        public Task ResetContactPrivacyRelationship()
        {
            return ResetContactPrivacyRelationship(HttpService.GetNewCancellationToken());
        }
        public async Task ResetContactPrivacyRelationship(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.resetContactPrivacyRelationship, "", cancellationToken, "2");
        }

        public Task Update()
        {
            return Update(HttpService.GetNewCancellationToken());
        }
        public async Task Update(CancellationToken cancellationToken)
        {
            await HttpService.Put(Self, this, cancellationToken, "2");
        }
    }
}
