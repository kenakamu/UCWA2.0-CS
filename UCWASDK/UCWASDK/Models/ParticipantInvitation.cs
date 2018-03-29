using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents an invitation to an existing conversation for an additional participant.
    /// This resource can be incoming or outgoing. If outgoing, the participantInvitation can be created using addParticipant. This resource assists in keeping track of the invitation status; for example, the invitation could be accepted, declined, or ignored. An outgoing participantInvitation will be in the 'Connecting' state while waiting for the recipient to accept or decline it. Note that if the recipient does not respond in approximately thirty seconds, the participantInvitation will complete with failure. Ultimately, the participantInvitation will complete with success or failure (in which case a reason is supplied). The participantInvitation will complete with success only after the participant appears in the roster. There is no incoming participantInvitation; it will instead appear as an onlineMeetingInvitation. 
    /// </summary>
    public class ParticipantInvitation : UCWAModelBase
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
        public From From { get { return Embedded.from; } }

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

            [JsonProperty("cancel")]
            internal Cancel cancel { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }

            [JsonProperty("from")]
            internal UCWAHref from { get; set; }

            [JsonProperty("participant")]
            internal UCWAHref participant { get; set; }

            [JsonProperty("to")]
            internal UCWAHref to { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("acceptedByParticipant")]
            internal AcceptedByParticipant[] acceptedByParticipants { get; set; }

            [JsonProperty("from")]
            internal From from { get; set; }
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

        public Task<From> GetFrom()
        {
            return GetFrom(HttpService.GetNewCancellationToken());
        }
        public Task<From> GetFrom(CancellationToken cancellationToken)
        {
            return HttpService.Get<From>(Links.from, cancellationToken);
        }

        public Task<Participant> GetParticipant()
        {
            return GetParticipant(HttpService.GetNewCancellationToken());
        }
        public Task<Participant> GetParticipant(CancellationToken cancellationToken)
        {
            return HttpService.Get<Participant>(Links.participant, cancellationToken);
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
