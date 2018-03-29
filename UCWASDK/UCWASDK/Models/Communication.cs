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
    /// Represents the dashboard for communication capabilities. 
    /// This resource exposes the modalities and settings available to the user, including the ability to join an onlineMeeting or create an ad-hoc onlineMeeting. Please note that this resource will be the sender for all events pertaining to conversations and modality invitations ( messagingInvitation or phoneAudioInvitation). 
    /// </summary>
    public class Communication : UCWAModelBase
    {
        [JsonProperty("conversationHistory")]
        public GenericPolicy ConversationHistory { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("supportedModalities")]
        public ModalityType[] SupportedModalities { get; set; }

        [JsonProperty("supportedMessageFormats")]
        public MessageFormat[] SupportedMessageFormats { get; set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("conversationLogs")]
            internal UCWAHref conversationLogs { get; set; } 

            [JsonProperty("conversations")]
            internal UCWAHref conversations { get; set; }

            [JsonProperty("joinOnlineMeeting")]
            internal JoinOnlineMeeting joinOnlineMeeting { get; set; }

            [JsonProperty("missedItems")]
            internal UCWAHref missedItems { get; set; }

            [JsonProperty("startMessaging")]
            internal StartMessaging2 startMessaging { get; set; }

            [JsonProperty("startOnlineMeeting")]
            internal StartOnlineMeeting startOnlineMeeting { get; set; }

            [JsonProperty("startPhoneAudio")]
            internal StartPhoneAudio2 startPhoneAudio { get; set; }
        }

        public Task<ConversationLogs> GetConversationLogs()
        {
            return GetConversationLogs(HttpService.GetNewCancellationToken());
        }
        public async Task<ConversationLogs> GetConversationLogs(CancellationToken cancellationToken)
        {
            return await HttpService.Get<ConversationLogs>(Links.conversationLogs, cancellationToken);
        }

        public Task<Conversations> GetConversations()
        {
            return GetConversations(HttpService.GetNewCancellationToken());
        }
        public async Task<Conversations> GetConversations(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Conversations>(Links.conversations, cancellationToken);
        }
        
        public Task JoinOnlineMeeting(string onlineMeetingUri, string subject, Importance importance)
        {
            return JoinOnlineMeeting(onlineMeetingUri, subject, importance, HttpService.GetNewCancellationToken());
        }
        public async Task JoinOnlineMeeting(string onlineMeetingUri, string subject, Importance importance, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(onlineMeetingUri) || string.IsNullOrEmpty(subject))
                return;
            
            JObject body = new JObject();
            body["onlineMeetingUri"] = onlineMeetingUri;
            body["subject"] = subject;
            body["importance"] = importance.ToString();
            body["operationid"] = Guid.NewGuid();
            body["threadid"] = Guid.NewGuid();

            await HttpService.Post(Links.joinOnlineMeeting, body, cancellationToken);
        }

        public Task<MissedItems> GetMissedItems()
        {
            return GetMissedItems(HttpService.GetNewCancellationToken());
        }
        public async Task<MissedItems> GetMissedItems(CancellationToken cancellationToken)
        {
            return await HttpService.Get<MissedItems>(Links.missedItems, cancellationToken);
        }

        public Task<string> StartMessaging(string sipName, string subject, Importance importance, string message)
        {
            return StartMessaging(sipName, subject, importance, message, HttpService.GetNewCancellationToken());
        }
        public async Task<string> StartMessaging(string sipName, string subject, Importance importance, string message, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sipName))
                return "";

            string encodedMessage = "";
            if (!string.IsNullOrEmpty(message))
            {
                byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(message);
                encodedMessage = System.Convert.ToBase64String(toEncodeAsBytes);
            }

            MessagingInvitation invitation = new Models.MessagingInvitation()
            {
                To = string.IsNullOrEmpty(sipName) ? null : sipName,
                Subject = subject,
                Importance = importance,
                OperationId = Guid.NewGuid().ToString(),
                ThreadId = Guid.NewGuid().ToString(),
                Links = string.IsNullOrEmpty(encodedMessage) ? null : new MessagingInvitation.InternalLinks()
                {
                    message = new UCWAHref() { Href = $"data:text/plain;base64,{encodedMessage}" }
                }
            };

            return await HttpService.Post(Links.startMessaging, invitation, cancellationToken);
        }

        public Task<string> StartOnlineMeeting(string subject, Importance importance)
        {
            return StartOnlineMeeting(subject, importance, HttpService.GetNewCancellationToken());
        }
        public async Task<string> StartOnlineMeeting(string subject, Importance importance, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(subject))
                return "";

            OnlineMeetingInvitation invitation = new OnlineMeetingInvitation()
            {
                Subject = subject,
                Importance = importance,
                OperationId = Guid.NewGuid().ToString(),
                ThreadId = Guid.NewGuid().ToString()
            };

            return await HttpService.Post(Links.startOnlineMeeting, invitation, cancellationToken);
        }

        public Task StartPhoneAudio(string phoneNumber, string to, string subject, Importance importance)
        {
            return StartPhoneAudio(phoneNumber, to, subject, importance, HttpService.GetNewCancellationToken());
        }
        public async Task StartPhoneAudio(string phoneNumber, string to, string subject, Importance importance, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(subject))
                return;
            
            PhoneAudioInvitation invitation = new PhoneAudioInvitation()
            {
                PhoneNumber = phoneNumber,
                To = to,
                Subject = subject,
                Importance = importance,
                OperationId = Guid.NewGuid().ToString(),
                ThreadId = Guid.NewGuid().ToString()
            };
 
            await HttpService.Post(Links.startPhoneAudio, invitation, cancellationToken);
        }

        public Task Update()
        {
            return Update(HttpService.GetNewCancellationToken());
        }
        public async Task Update(CancellationToken cancellationToken)
        {
            await HttpService.Put(Self, this, cancellationToken);
        }
    }
}
