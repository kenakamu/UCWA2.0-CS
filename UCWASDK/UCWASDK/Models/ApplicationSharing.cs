using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the application sharing modality in the corresponding conversation. 
    /// In this version of the API, viewing or sharing of content is not supported. However, this information can be useful for UI updates or for letting the user contact the sharer to let the user see the content that is being shared. The absence of this resource indicates that no one is sharing a program or their screen. 
    /// </summary>
    public class ApplicationSharing : UCWAModelBase
    {
        [JsonProperty("state")]
        public string State { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }
        
        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("applicationSharer")]
            internal UCWAHref applicationSharer { get; set; }
            
            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }
        }

        public async Task<ApplicationSharer> GetApplicationSharer()
        {
            return await HttpService.Get<ApplicationSharer>(Links.applicationSharer);
        }

        public async Task<Conversation> GetConversation()
        {
            return await HttpService.Get<Conversation>(Links.conversation);
        }
    }
}
