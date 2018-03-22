using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// The (archived) messages from Exchange. 
    /// </summary>
    public class ConversationLogTranscripts : UCWAModelBase
    {
        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; internal set; }
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public ConversationLogTranscript ConversationLogTranscript { get { return Embedded.conversationLogTranscript; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("nextConversationLogTranscripts")]
            internal UCWAHref nextConversationLogTranscripts { get; set; }
            public NextConversationLogTranscripts NextConversationLogTranscripts { get { return HttpService.Get<NextConversationLogTranscripts>(nextConversationLogTranscripts, HttpService.GetNewCancellationToken()).Result; } }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("conversationLogTranscript")]
            internal ConversationLogTranscript conversationLogTranscript { get; set; }
        }

        public Task<NextConversationLogTranscripts> GetNextConversationLogTranscripts()
        {
            return GetNextConversationLogTranscripts(HttpService.GetNewCancellationToken());
        }
        public async Task<NextConversationLogTranscripts> GetNextConversationLogTranscripts(CancellationToken cancellationToken)
        {
            return await HttpService.Get<NextConversationLogTranscripts>(Links.nextConversationLogTranscripts, cancellationToken);
        }
    }
}
