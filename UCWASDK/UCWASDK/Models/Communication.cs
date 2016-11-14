using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<ConversationLogs> GetConversationLogs()
        {
            return await HttpService.Get<ConversationLogs>(Links.conversationLogs);
        }

        public async Task<Conversations> GetConversations()
        {
            return await HttpService.Get<Conversations>(Links.conversations);
        }
        
        public async Task JoinOnlineMeeting(string onlineMeetingUri, string subject, Importance importance)
        {
            if (string.IsNullOrEmpty(onlineMeetingUri) || string.IsNullOrEmpty(subject))
                return;
            
            JObject body = new JObject();
            body["onlineMeetingUri"] = onlineMeetingUri;
            body["subject"] = subject;
            body["importance"] = importance.ToString();
            body["operationid"] = Guid.NewGuid();
            body["threadid"] = Guid.NewGuid();

            await HttpService.Post(Links.joinOnlineMeeting, body);
        }

        public async Task<MissedItems> GetMissedItems()
        {
            return await HttpService.Get<MissedItems>(Links.missedItems);
        }

        public async Task<string> StartMessaging(string sipName, string subject, Importance importance, string message)
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

            return await HttpService.Post(Links.startMessaging, invitation);
        }

        public async Task<string> StartOnlineMeeting(string subject, Importance importance)
        {
            if (string.IsNullOrEmpty(subject))
                return "";

            OnlineMeetingInvitation invitation = new Models.OnlineMeetingInvitation()
            {
                Subject = subject,
                Importance = importance,
                OperationId = Guid.NewGuid().ToString(),
                ThreadId = Guid.NewGuid().ToString()
            };

            return await HttpService.Post(Links.startOnlineMeeting, invitation);
        }

        public async Task StartPhoneAudio(string phoneNumber, string to, string subject, Importance importance)
        {
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(subject))
                return;
            
            PhoneAudioInvitation invitation = new Models.PhoneAudioInvitation()
            {
                PhoneNumber = phoneNumber,
                To = to,
                Subject = subject,
                Importance = importance,
                OperationId = Guid.NewGuid().ToString(),
                ThreadId = Guid.NewGuid().ToString()
            };
 
            await HttpService.Post(Links.startPhoneAudio, invitation);
        }

        public async Task Update()
        {
            await HttpService.Put(Self, this);
        }
    }
}
