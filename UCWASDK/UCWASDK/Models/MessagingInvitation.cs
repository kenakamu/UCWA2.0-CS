using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an invitation to a conversation for the messaging modality.
    /// This resource can be incoming or outgoing. If outgoing, the messagingInvitation can be created in one of two ways. First, startMessaging will create a messagingInvitation that also creates a conversation. Second, addMessaging will attempt to add the messaging modality to an existing conversation. This resource assists in keeping track of the invitation status; for example, the invitation could be accepted, declined, or ignored. An outgoing messagingInvitation will be in the 'Connecting' state while waiting for the recipient to accept or decline it; during this time, the messagingInvitation can be terminated using cancel. Note that if the recipient does not respond in approximately thirty seconds, the messagingInvitation will complete with failure. Ultimately, the messagingInvitation will complete with success or failure (in which case a reason is supplied). If the messagingInvitation succeeds, the participant that accepts it will be provided ( acceptedByParticipant). If incoming, the messagingInvitation can create a new conversation or attempt to add the messaging modality to an existing conversation. 
    /// </summary>
    public class MessagingInvitation : UCWAModelBase
    {
        [JsonProperty("direction")]
        public Direction? Direction { get; internal set; }

        [JsonProperty("importance")]
        public Importance Importance { get; internal set; }

        [JsonProperty("operationId")]
        public string OperationId { get; internal set; }

        [JsonProperty("state")]
        public InvitationState? State { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; internal set; }

        [JsonProperty("threadId")]
        public string ThreadId { get; internal set; }

        [JsonProperty("to")]
        public string To { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string CustomContent { get { return Links.customContent == null ? "" : WebUtility.UrlDecode(Links.customContent.Href.Split(',')[1]); } }

        [JsonIgnore]
        public string Message { get { return Links.message == null ? "" : WebUtility.UrlDecode(Links.message.Href.Split(',')[1]); } }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public AcceptedByParticipant[] AcceptedByParticipants { get { return Embedded.acceptedByParticipants; } }

        [JsonIgnore]
        public Participant From { get { return Embedded.from; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("customContent")]
            internal UCWAHref customContent { get; set; }

            [JsonProperty("message")]
            internal UCWAHref message { get; set; }

            [JsonProperty("from")]
            internal UCWAHref from { get; set; }

            [JsonProperty("accept")]
            internal Accept accept { get; set; }

            [JsonProperty("acceptedByContact")]
            internal UCWAHref acceptedByContact { get; set; }

            [JsonProperty("cancel")]
            internal Cancel cancel { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }

            [JsonProperty("decline")]
            internal UCWAHref decline { get; set; }

            [JsonProperty("derivedMessaging")]
            internal UCWAHref derivedMessaging { get; set; }

            [JsonProperty("messaging")]
            internal UCWAHref messaging { get; set; }

            [JsonProperty("onBehalfOf")]
            internal UCWAHref onBehalfOf { get; set; }

            [JsonProperty("to")]
            internal UCWAHref to { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("acceptedByParticipant")]
            internal AcceptedByParticipant[] acceptedByParticipants { get; set; }

            [JsonProperty("from")]
            internal Participant from { get; set; }
        }

        public Task Accept()
        {
            return Accept(HttpService.GetNewCancellationToken());
        }
        public Task Accept(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.accept, "", cancellationToken);
        }

        public Task<AcceptedByContact> GetAcceptedByContact()
        {
            return GetAcceptedByContact(HttpService.GetNewCancellationToken());
        }
        public Task<AcceptedByContact> GetAcceptedByContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<AcceptedByContact>(Links.acceptedByContact, cancellationToken);
        }

        public Task Cancel()
        {
            return Cancel(HttpService.GetNewCancellationToken());
        }
        public Task Cancel(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.cancel, "", cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }
        public Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public Task Decline(CallDeclineReason reason)
        {
            return Decline(reason, HttpService.GetNewCancellationToken());
        }
        public Task Decline(CallDeclineReason reason, CancellationToken cancellationToken)
        {
            CallDecline callDecline = new CallDecline()
            {
                Reason = reason
            };
            return HttpService.Post(Links.decline, callDecline, cancellationToken);
        }

        public Task<From> GetFrom()
        {
            return GetFrom(HttpService.GetNewCancellationToken());
        }
        public Task<From> GetFrom(CancellationToken cancellationToken)
        {
            return HttpService.Get<From>(Links.from, cancellationToken);
        }

        public Task<DerivedMessaging> GetDerivedMessaging()
        {
            return GetDerivedMessaging(HttpService.GetNewCancellationToken());
        }
        public Task<DerivedMessaging> GetDerivedMessaging(CancellationToken cancellationToken)
        {
            return HttpService.Get<DerivedMessaging>(Links.derivedMessaging, cancellationToken);
        }

        public Task<Messaging> GetMessaging()
        {
            return GetMessaging(HttpService.GetNewCancellationToken());
        }
        public Task<Messaging> GetMessaging(CancellationToken cancellationToken)
        {
            return HttpService.Get<Messaging>(Links.messaging, cancellationToken);
        }

        public Task<OnBehalfOf> GetOnBehalfOf()
        {
            return GetOnBehalfOf(HttpService.GetNewCancellationToken());
        }
        public Task<OnBehalfOf> GetOnBehalfOf(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnBehalfOf>(Links.onBehalfOf, cancellationToken);
        }

        public Task<To> GetTo()
        {
            return GetTo(HttpService.GetNewCancellationToken());
        }
        public Task<To> GetTo(CancellationToken cancellationToken)
        {
            return HttpService.Get<To>(Links.to, cancellationToken);
        }
    }
}
