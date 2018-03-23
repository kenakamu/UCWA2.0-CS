using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an invitation to a conversation for the phoneAudio modality.
    /// This resource can be incoming or outgoing. If outgoing, the phoneAudioInvitation can be created in one of two ways. First, startPhoneAudio will create a phoneAudioInvitation that also creates a conversation.Second, addPhoneAudio will attempt to add the phoneAudio modality to an existing conversation. An outgoing invitation will first ring the user on the supplied phone number. After the user answers the call, the phoneAudioInvitation will be then be sent to the target. This resource assists in keeping track of the invitation status; for example, the invitation could be forwarded or sent to all members of the invitee's team (team ring). Ultimately, the phoneAudioInvitation will complete with success or failure (in which case a reason is supplied). If the phoneAudioInvitation succeeds, the participant ( acceptedByParticipant) who accepts the call can be different from the original target ( to). The application can determine when the target is different by comparing the contact in the acceptedByParticipant with the contact represented by the to resource. In the case of addPhoneAudio, the corresponding phoneAudioInvitation will cause the creation of a new, related conversation ( derivedConversation) with the new remote participants. If incoming, the phoneAudioInvitation might create a new conversation or attempt to add the phoneAudio modality to an existing conversation. Note that a phoneAudioInvitation cannot be accepted using the API; instead it is accepted when the user answers the phone call. It can, however, be declined using the API. Additionally, an incoming phoneAudioInvitation can be the result of being transferred by a contact ( transferredBy) or by being forwarded by by a contact ( forwardedBy). It can also be received on behalf of another user ( onBehalfOf) of the calling party ( from). 
    /// </summary>
    public class PhoneAudioInvitation : UCWAModelBase
    {
        [JsonProperty("direction")]
        public Direction? Direction { get; internal set; }

        [JsonProperty("importance")]
        public Importance Importance { get; internal set; }

        [JsonProperty("joinAudioMuted")]
        public bool JoinAudioMuted { get; internal set; }

        [JsonProperty("operationId")]
        public string OperationId { get; internal set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; internal set; }

        [JsonProperty("privateLine")]
        public bool PrivateLine { get; internal set; }

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
      
            [JsonProperty("derivedConversation")]
            internal UCWAHref derivedConversation { get; set; }
      
            [JsonProperty("derivedPhoneAudio")]
            internal UCWAHref derivedPhoneAudio { get; set; }
            
            [JsonProperty("forwardedBy")]
            internal UCWAHref forwardedBy { get; set; }
      
            [JsonProperty("from")]
            internal UCWAHref from { get; set; }
      
            [JsonProperty("onBehalfOf")]
            internal UCWAHref onBehalfOf { get; set; }
      
            [JsonProperty("phoneAudio")]
            internal UCWAHref phoneAudio { get; set; }
      
            [JsonProperty("to")]
            internal UCWAHref to { get; set; }
      
            [JsonProperty("transferredBy")]
            internal UCWAHref transferredBy { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("acceptedByParticipant")]
            internal AcceptedByParticipant[] acceptedByParticipants { get; set; }

            [JsonProperty("from")]
            internal Participant from { get; set; }
        }

        public async Task Accept()
        {
            await Accept(HttpService.GetNewCancellationToken());
        }

        public async Task Accept(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.accept, "", cancellationToken);
        }

        public Task<AcceptedByContact> GetAcceptedByContact()
        {
            return GetAcceptedByContact(HttpService.GetNewCancellationToken());
        }

        public Task<AcceptedByContact> GetAcceptedByContact(CancellationToken cancellationToken)
        {
            return HttpService.Get<AcceptedByContact>(Links.acceptedByContact, cancellationToken);
        }

        public async Task Cancel()
        {
            await Cancel(HttpService.GetNewCancellationToken());
        }

        public async Task Cancel(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.cancel, "", cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }

        public Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public async Task Decline(CallDeclineReason reason)
        {
            await Decline(reason, HttpService.GetNewCancellationToken());
        }

        public async Task Decline(CallDeclineReason reason, CancellationToken cancellationToken)
        {
            CallDecline callDecline = new CallDecline()
            {
                Reason = reason
            };
            await HttpService.Post(Links.decline, callDecline, cancellationToken);
        }

        public Task<DerivedConversation> GetDerivedConversation()
        {
            return GetDerivedConversation(HttpService.GetNewCancellationToken());
        }

        public Task<DerivedConversation> GetDerivedConversation(CancellationToken cancellationToken)
        {
            return HttpService.Get<DerivedConversation>(Links.derivedConversation, cancellationToken);
        }

        public Task<DerivedPhoneAudio> GetDerivedPhoneAudio()
        {
            return GetDerivedPhoneAudio(HttpService.GetNewCancellationToken());
        }

        public Task<DerivedPhoneAudio> GetDerivedPhoneAudio(CancellationToken cancellationToken)
        {
            return HttpService.Get<DerivedPhoneAudio>(Links.derivedPhoneAudio, cancellationToken);
        }

        public Task<ForwardedBy> GetForwardedBy()
        {
            return GetForwardedBy(HttpService.GetNewCancellationToken());
        }

        public Task<ForwardedBy> GetForwardedBy(CancellationToken cancellationToken)
        {
            return  HttpService.Get<ForwardedBy>(Links.forwardedBy, cancellationToken);
        }

        public Task<From> GetFrom()
        {
            return GetFrom(HttpService.GetNewCancellationToken());
        }

        public Task<From> GetFrom(CancellationToken cancellationToken)
        {
            return HttpService.Get<From>(Links.from, cancellationToken);
        }

        public Task<OnBehalfOf> GetOnBehalfOf()
        {
            return GetOnBehalfOf(HttpService.GetNewCancellationToken());
        }

        public Task<OnBehalfOf> GetOnBehalfOf(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnBehalfOf>(Links.onBehalfOf, cancellationToken);
        }

        public Task<PhoneAudio> GetPhoneAudio()
        {
            return GetPhoneAudio(HttpService.GetNewCancellationToken());
        }

        public Task<PhoneAudio> GetPhoneAudio(CancellationToken cancellationToken)
        {
            return HttpService.Get<PhoneAudio>(Links.phoneAudio, cancellationToken);
        }

        public Task<To> GetTo()
        {
            return GetTo(HttpService.GetNewCancellationToken());
        }

        public Task<To> GetTo(CancellationToken cancellationToken)
        {
            return HttpService.Get<To>(Links.to, cancellationToken);
        }

        public Task<TransferredBy> GetTransferredBy()
        {
            return GetTransferredBy(HttpService.GetNewCancellationToken());
        }

        public Task<TransferredBy> GetTransferredBy(CancellationToken cancellationToken)
        {
            return HttpService.Get<TransferredBy>(Links.transferredBy, cancellationToken);
        }
    }
}
