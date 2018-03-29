using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's past conversation logs (both peer-to-peer and conferences). 
    /// </summary>
    public class ConversationLogs : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("conversationLog")]
            internal UCWAHref[] conversationLog { get; set; }
        }

        public Task<List<ConversationLog>> GetConversationLogs()
        {
            return GetConversationLogs(HttpService.GetNewCancellationToken());
        }
        public async Task<List<ConversationLog>> GetConversationLogs(CancellationToken cancellationToken)
        {
            return await HttpService.GetList<ConversationLog>(Links.conversationLog, cancellationToken);
        }
    }
}
