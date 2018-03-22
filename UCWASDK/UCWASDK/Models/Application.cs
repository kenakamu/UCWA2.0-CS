using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents your real-time communication application. 
    /// This resource represents an application on one of the user's devices. This resource is used as an entry point to start to communicate and collaborate. The application gives all supported capabilities and embeds the resources associated with the following relationships: me, people, communication, onlineMeetings.The application resource will expire if the application remains idle (i.e. no HTTP requests are received for a period of time from the application) for a certain amount of time. The expiration time varies depending upon whether the application makes use of the event channel (by issuing pending GETs on events) or not.
    /// </summary>
    public class Application : UCWAModelBase
    {
        [JsonProperty("culture")]
        public string Culture { get; internal set; }

        [JsonProperty("endpointId")]
        public string EndpointId { get; internal set; }

        [JsonProperty("instanceId")]
        public string InstanceId { get; internal set; }

        [JsonProperty("userAgent")]
        public string UserAgent { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Communication Communication { get { return Embedded?.communication; } }

        [JsonIgnore]
        public Me Me { get { return Embedded?.me; } }

        [JsonIgnore]
        public People People { get { return Embedded?.people; } }

        [JsonIgnore]
        public OnlineMeetings OnlineMeetings { get { return Embedded?.onlineMeetings; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("batch")]
            private UCWAHref batch { get; set; }
            public string Batch { get { return batch?.Href; } }

            [JsonProperty("events")]
            private UCWAHref events { get; set; }
            public string Events { get { return events?.Href; } }

            [JsonProperty("policies")]
            internal UCWAHref policies { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("communication")]
            internal Communication communication { get; set; }

            [JsonProperty("me")]
            internal Me me { get; set; }

            [JsonProperty("people")]
            internal People people { get; set; }

            [JsonProperty("onlineMeetings")]
            internal OnlineMeetings onlineMeetings { get; set; }            
        }

        public async Task<Policies> GetPolicies()
        {
            return await HttpService.Get<Policies>(Links.policies);
        }

        public async Task<Application>Get()
        {
            return await HttpService.Get<Application>(Self, HttpService.GetNewCancellationToken());
        }
        public async Task<Application> Get(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Application>(Self, cancellationToken);
        }
        public async Task Delete()
        {
            await HttpService.Delete(Self);
        }
    }
}
