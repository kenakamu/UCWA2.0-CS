using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a transcript within a conversationLog. 
    /// </summary>
    public class ConversationLogTranscript : UCWAModelBase
    {
        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; internal set; }
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public AudioTranscript AudioTranscript { get { return Embedded.audioTranscript; } }

        [JsonIgnore]
        public ErrorTranscript ErrorTranscript { get { return Embedded.errorTranscript; } }

        [JsonIgnore]
        public MessageTranscript MessageTranscript { get { return Embedded.messageTranscript; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref contact { get; set; }

            [JsonProperty("me")]
            internal UCWAHref me { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("audioTranscript")]
            internal AudioTranscript audioTranscript { get; set; }

            [JsonProperty("errorTranscript")]
            internal ErrorTranscript errorTranscript { get; set; }

            [JsonProperty("messageTranscript")]
            internal MessageTranscript messageTranscript { get; set; }
        }

        public async Task<Contact> GetContact()
        {
            return await HttpService.Get<Contact>(Links.contact);
        }

        public async Task<Me> GetMe()
        {
            return await HttpService.Get<Me>(Links.me);
        }
    }
}
