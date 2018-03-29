using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the phone audio modality in a conversation. 
    /// Phone audio refers to communication that is delivered via a public switched telephone network (PSTN). When present, the resource indicates the status of the phone audio channel and can provide capabilities to start or stop the phone call via a user-supplied phone number, as well as to place the call on hold. phoneAudio will be updated whenever the state and capabilities of the modality change. 
    /// </summary>
    public class PhoneAudio : UCWAModelBase
    {
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

            [JsonProperty("addPhoneAudio")]
            internal AddPhoneAudio addPhoneAudio { get; set; }
     
            [JsonProperty("conversation")]
            internal UCWAHref conversation { get; set; }
     
            [JsonProperty("holdPhoneAudio")]
            internal HoldPhoneAudio holdPhoneAudio { get; set; }
     
            [JsonProperty("resumePhoneAudio")]
            internal ResumePhoneAudio resumePhoneAudio { get; set; }
     
            [JsonProperty("stopPhoneAudio")]
            internal StopPhoneAudio stopPhoneAudio { get; set; }
        }

        public async Task AddPhoneAudio(string sipName, string phoneNumber)
        {
            await AddPhoneAudio(sipName, phoneNumber, HttpService.GetNewCancellationToken());
        }

        public async Task AddPhoneAudio(string sipName, string phoneNumber, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sipName) && string.IsNullOrEmpty(phoneNumber))
                return;
            
            PhoneAudioInvitation invitaion = new Models.PhoneAudioInvitation()
            {
                To = string.IsNullOrEmpty(sipName) ? null : sipName,
                PhoneNumber = string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber,
                OperationId = Guid.NewGuid().ToString()
            };

            await HttpService.Post(Links.addPhoneAudio, invitaion, cancellationToken);
        }

        public Task<Conversation> GetConversation()
        {
             return GetConversation(HttpService.GetNewCancellationToken());
        }

        public Task<Conversation> GetConversation(CancellationToken cancellationToken)
        {
            return HttpService.Get<Conversation>(Links.conversation, cancellationToken);
        }

        public async Task HoldPhoneAudio()
        {
            await HoldPhoneAudio(HttpService.GetNewCancellationToken());
        }

        public async Task HoldPhoneAudio(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.holdPhoneAudio, "", cancellationToken);
        }

        public async Task ResumePhoneAudio()
        {
            await ResumePhoneAudio(HttpService.GetNewCancellationToken());
        }

        public async Task ResumePhoneAudio(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.resumePhoneAudio, "", cancellationToken);
        }

        public async Task StopPhoneAudio()
        {
            await StopPhoneAudio(HttpService.GetNewCancellationToken());
        }

        public async Task StopPhoneAudio(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.stopPhoneAudio, "", cancellationToken);
        }
    }
}
