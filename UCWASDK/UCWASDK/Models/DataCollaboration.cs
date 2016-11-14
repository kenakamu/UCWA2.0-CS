using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the data collaboration modality in the corresponding conversation. 
    /// In this version of the API, sharing or viewing content is not supported. 
    /// </summary>
    public class DataCollaboration : UCWAModelBase
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

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }
        }

        public async Task<Conversation> GetConversation()
        {
            return await HttpService.Get<Conversation>(Links.conversation);
        }
    }
}
