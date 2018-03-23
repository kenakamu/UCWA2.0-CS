using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// The (archived) messages from Exchange. 
    /// </summary>
    public class NextConversationLogTranscripts : UCWAModelBase
    {        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public ConversationLogTranscript[] ConversationLogTranscripts { get { return Embedded.conversationLogTranscripts; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("nextConversationLogTranscripts")]
            internal UCWAHref nextConversationLogTranscripts { get; set; }
       }

        internal class InternalEmbedded
        {
            [JsonProperty("conversationLogTranscript")]
            internal ConversationLogTranscript[] conversationLogTranscripts { get; set; }               
        }

        public Task<NextConversationLogTranscripts> GetNextConversationLogTranscripts()
        {
            return GetNextConversationLogTranscripts(HttpService.GetNewCancellationToken());
        }
        public Task<NextConversationLogTranscripts> GetNextConversationLogTranscripts(CancellationToken cancellationToken)
        {
            return HttpService.Get<NextConversationLogTranscripts>(Links.nextConversationLogTranscripts, cancellationToken);
        }
    }
}
