using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's view of an instance of past conversation. 
    /// </summary>
    public class ConversationLog : UCWAModelBase
    {
        [JsonProperty("creationTime")]
        public DateTime CreationTime { get; internal set; }

        [JsonProperty("direction")]
        public Direction Direction { get; internal set; }

        [JsonProperty("importance")]
        public Importance Importance { get; internal set; }

        [JsonProperty("modalities")]
        public ConversationModalityType[] Modalities { get; internal set; }

        [JsonProperty("onlineMeetingUri")]
        public string OnlineMeetingUri { get; internal set; }

        [JsonProperty("previewMessage")]
        public string PreviewMessage { get; internal set; }

        [JsonProperty("status")]
        public ConversationLogStatus Status { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; internal set; }

        [JsonProperty("threadId")]
        public string ThreadId { get; internal set; }

        [JsonProperty("totalRecipientsCount")]
        public int TotalRecipientsCount { get; internal set; }

        [JsonProperty("type")]
        public ConversationLogType Type { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public ConversationLogRecipient[] ConversationLogRecipients { get { return Embedded.conversationLogRecipients; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("continueMessaging")]
            internal ContinueMessaging continueMessaging { get; set; }

            [JsonProperty("continuePhoneAudio")]
            internal ContinuePhoneAudio continuePhoneAudio { get; set; }

            [JsonProperty("conversationLogTranscripts")]
            internal UCWAHref conversationLogTranscripts { get; set; }

            [JsonProperty("markAsRead")]
            internal MarkAsRead markAsRead { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("conversationLogRecipient")]
            internal ConversationLogRecipient[] conversationLogRecipients { get; set; }
        }
        
        public async Task ContinueMessaging(MessageFormat messageFormat, string message)
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

            MessagingInvitation messagingInvitation = new Models.MessagingInvitation()
            {
                OperationId = Guid.NewGuid().ToString(),
                Links = new MessagingInvitation.InternalLinks()
                {
                    message = new UCWAHref() { Href = messageBody }
                }
            };

            await HttpService.Post(Links.continueMessaging, messagingInvitation);
        }

        public async Task ContinuePhoneAudio(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return;

            PhoneAudioInvitation phoneAudioInvitation = new PhoneAudioInvitation()
            {
                OperationId = Guid.NewGuid().ToString(),
                PhoneNumber = phoneNumber
            };
            
            await HttpService.Post(Links.continuePhoneAudio, phoneAudioInvitation);
        }

        public async Task<ConversationLogTranscripts> GetConversationLogTranscripts()
        {
            return await HttpService.Get<ConversationLogTranscripts>(Links.conversationLogTranscripts);
        }

        public async Task MarkAsRead()
        {
            await HttpService.Post(Links.markAsRead, "");
        }

        public async Task Delete()
        {
            await HttpService.Delete(Self);
        }
    }
}
