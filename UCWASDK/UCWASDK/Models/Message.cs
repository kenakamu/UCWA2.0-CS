using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an instant message sent or received by the local participant. 
    /// Message is started in the event channel for both incoming and outgoing scenarios. For outgoing instant messages, the message delivery status can be tracked via the event channel. When the message is part of a peer-to-peer conversation, the delivery status merely indicates whether the message was delivered to the remote participant. In the multi-party case, the delivery status indicates which of the remote participants failed to receive the message. For incoming instant messages, message captures information such as the remote participant who sent the message, the time stamp, and the body of the message. If an incoming instant message is received by UCWA but is not fetched by the application via the event channel in a timely fashion (within 30 seconds), the message will time out and the app will not be able to receive it. Additionally, the sender of the message will be notified that the message was not received.
    /// </summary>
    public class Message : UCWAModelBase
    {
        [JsonProperty("direction")]
        public Direction Direction { get; internal set; }

        [JsonProperty("operationId")]
        public string OperationId { get; internal set; }

        [JsonProperty("status")]
        public Status Status { get; internal set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; internal set; }
        
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        private string text;
        [JsonIgnore]
        public string Text {
            get
            {
                if (!string.IsNullOrEmpty(text))
                    return text;
                if (Links.htmlMessage != null)
                    return XDocument.Parse(WebUtility.UrlDecode(Links.htmlMessage?.Href?.Split(',')[1])).Element("span").Value;
                else if (Links.plainMessage != null)
                    return WebUtility.UrlDecode(Links.plainMessage?.Href?.Split(',')[1]);
                else
                    return "";
            }
            set
            {
                text = value;
            }
        }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("htmlMessage")]
            internal UCWAHref htmlMessage { get; set; }

            [JsonProperty("plainMessage")]
            internal UCWAHref plainMessage { get; set; }

            [JsonProperty("contact")]
            internal UCWAHref contact { get; set; }

            [JsonProperty("failedDeliveryParticipant")]
            internal UCWAHref[] failedDeliveryParticipant { get; set; }

            [JsonProperty("messaging")]
            internal UCWAHref messaging { get; set; }

            [JsonProperty("participant")]
            internal UCWAHref participant { get; set; }
        }
        
        public Task<Contact> GetContact()
        {
            return GetContact(HttpService.GetNewCancellationToken());
        }
        public async Task<Contact> GetContact(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Contact>(Links.contact, cancellationToken);
        }

        public Task<List<FailedDeliveryParticipant>> GetFailedDeliveryParticipants()
        {
            return GetFailedDeliveryParticipants(HttpService.GetNewCancellationToken());
        }
        public async Task<List<FailedDeliveryParticipant>> GetFailedDeliveryParticipants(CancellationToken cancellationToken)
        {
            return await HttpService.GetList<FailedDeliveryParticipant>(Links.failedDeliveryParticipant, cancellationToken);
        }

        public Task<Messaging> GetMessaging()
        {
            return GetMessaging(HttpService.GetNewCancellationToken());
        }
        public async Task<Messaging> GetMessaging(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Messaging>(Links.messaging, cancellationToken);
        }

        public Task<Participant> GetParticipant()
        {
            return GetParticipant(HttpService.GetNewCancellationToken());
        }
        public async Task<Participant> GetParticipant(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Participant>(Links.participant, cancellationToken);
        }
    }
}
