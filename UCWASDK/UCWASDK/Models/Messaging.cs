using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the instant messaging modality in a conversation. 
    /// The presence of messaging in a conversation indicates that the application can use the instant messaging modality. When present, the resource can be used to determine the status of the instant messaging channel, to start or stop instant messaging, as well as to send a single message or a typing notification. The messaging resource is updated whenever message formats are negotiated or the state and capabilities of the modality are changed.
    /// </summary>
    public class Messaging : UCWAModelBase
    {
        [JsonProperty("negotiatedMessageFormats")]
        public MessageFormat[] NegotiatedMessageFormats { get; internal set; }

        [JsonProperty("state")]
        public string State { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("addMessaging")]
            internal AddMessaging addMessaging { get; set; }

            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }

            [JsonProperty("sendMessage")]
            internal SendMessage sendMessage { get; set; }
            public string SendMessage { get { return sendMessage?.Href; } }

            [JsonProperty("setIsTyping")]
            internal SetIsTyping setIsTyping { get; set; }

            [JsonProperty("stopMessaging")]
            internal StopMessaging stopMessaging { get; set; }

            [JsonProperty("typingParticipants")]
            internal UCWAHref typingParticipants { get; set; }
        }

        public Task AddMessaging(MessageFormat messageFormat, string message)
        {
            return AddMessaging(messageFormat, message, HttpService.GetNewCancellationToken());
        }
        public async Task AddMessaging(MessageFormat messageFormat, string message, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(message))
                return;

            var messageBody = "";
            if (messageFormat == MessageFormat.Plain)
                messageBody += "data:text/plain;base64,";
            else
                messageBody += "data:html/plain;base64,";

            byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(message);
            string encodedMessage = System.Convert.ToBase64String(toEncodeAsBytes);
            messageBody += encodedMessage;

            MessagingInvitation messagingInvitation = new MessagingInvitation()
            {
                OperationId = Guid.NewGuid().ToString(),
                Links = new MessagingInvitation.InternalLinks()
                {
                    message = new UCWAHref() { Href = messageBody }
                }
            };

            await HttpService.Post(Links.addMessaging, messagingInvitation, cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
            return GetConversation(HttpService.GetNewCancellationToken());
        }
        public async Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public Task SendMessage(string message)
        {
            return SendMessage(message, HttpService.GetNewCancellationToken());
        }
        public Task SendMessage(string message, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(message))
                return Task.FromResult<object>(null);

            return HttpService.Post(Links.SendMessage, message, cancellationToken);
        }

        public Task StopMessaging()
        {
            return StopMessaging(HttpService.GetNewCancellationToken());
        }
        public Task StopMessaging(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.stopMessaging, "", cancellationToken);
        }

        public Task SetIsTyping()
        {
            return SetIsTyping(HttpService.GetNewCancellationToken());
        }
        public Task SetIsTyping(CancellationToken cancellationToken)
        {
            return HttpService.Post(Links.setIsTyping, "", cancellationToken);
        }

        public Task<TypingParticipants> GetTypingParticipants()
        {
            return GetTypingParticipants(HttpService.GetNewCancellationToken());
        }
        public Task<TypingParticipants> GetTypingParticipants(CancellationToken cancellationToken)
        {
            return HttpService.Get<TypingParticipants>(Links.typingParticipants, cancellationToken);
        }
    }
}
