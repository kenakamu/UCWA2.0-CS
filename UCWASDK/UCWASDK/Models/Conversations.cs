using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's ongoing conversations. 
    /// </summary>
    public class Conversations : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref[] conversation { get; set; }
        }

        public async Task<List<Conversation>> GetConversations()
        {
            return await HttpService.GetList<Conversation>(Links.conversation);
        }
    }
}
