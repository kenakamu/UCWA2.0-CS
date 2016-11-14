using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the version two of MyGroupMembershipsResource (a collection of groupMembership resources) 
    /// The version two supports adding single contact to a particular group and removing a contact from the buddy list (all groups associated) 
    /// </summary>
    public class MyGroupMemberships2 : UCWAModelBase
    {        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public MyGroupMembership2[] GroupMemberships { get { return Embedded.groupMembership; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("removeContactFromAllGroups")]
            internal UCWAHref removeContactFromAllGroups { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("myGroupMembership")]
            internal MyGroupMembership2[] groupMembership { get; set; }
        }

        public async Task AddContact(string sipName, string groupId)
        {
            if (string.IsNullOrEmpty(sipName) || string.IsNullOrEmpty(groupId))
                return;

            var uri = Self + "?contactUri=" + sipName + "&groupId=" + groupId;

            await HttpService.Post(uri, "", "2");
        }

        public async Task RemoveContactFromAllGroups(string sipName)
        {
            if (string.IsNullOrEmpty(sipName))
                return;
            
            var uri = Links.removeContactFromAllGroups.Href + "?contactUri=" + sipName;

            await HttpService.Post(uri, "", "2");
        }
    }
}
