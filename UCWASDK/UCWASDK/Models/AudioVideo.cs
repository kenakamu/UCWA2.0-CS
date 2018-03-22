﻿using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the audio/video modality in the corresponding conversation. 
    /// In this version of the API, establishing audio/video calls is not supported. 
    /// </summary>
    public class AudioVideo : UCWAModelBase
    {
        [JsonProperty("state")]
        public string State { get; internal set; }

        [JsonProperty("supportsReplaces")]
        public string SupportsReplaces { get; internal set; }

        [JsonProperty("videoSourcesAllowed")]
        public VideoSourcesAllowed VideoSourcesAllowed { get; internal set; }

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

            [JsonProperty("videoLockedOnParticipant")]
            internal UCWAHref videoLockedOnParticipant { get; set; }
        }
        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }

        public async Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public Task<VideoLockedOnParticipant> GetVideoLockedOnParticipant()
        {
            return GetVideoLockedOnParticipant(HttpService.GetNewCancellationToken());
        }
        public async Task<VideoLockedOnParticipant> GetVideoLockedOnParticipant(CancellationToken cancellationToken)
        {
            return await HttpService.Get<VideoLockedOnParticipant>(Links.videoLockedOnParticipant, cancellationToken);
        }
    }
}
