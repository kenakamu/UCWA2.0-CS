using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the dashboard for viewing and scheduling online meetings. 
    /// The onlineMeetings resource exposes the meetings and settings available to the user, including the ability to create a new myOnlineMeeting. 
    /// </summary>
    public class OnlineMeetings : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("myAssignedOnlineMeeting")]
            internal UCWAHref myAssignedOnlineMeeting { get; set; }
      
            [JsonProperty("myOnlineMeetings")]
            internal UCWAHref myOnlineMeetings { get; set; }
      
            [JsonProperty("onlineMeetingDefaultValues")]
            internal UCWAHref onlineMeetingDefaultValues { get; set; }
      
            [JsonProperty("onlineMeetingEligibleValues")]
            internal UCWAHref onlineMeetingEligibleValues { get; set; }
      
            [JsonProperty("onlineMeetingInvitationCustomization")]
            internal UCWAHref onlineMeetingInvitationCustomization { get; set; }
      
            [JsonProperty("onlineMeetingPolicies")]
            internal UCWAHref onlineMeetingPolicies { get; set; }
      
            [JsonProperty("phoneDialInInformation")]
            internal UCWAHref phoneDialInInformation { get; set; }
        }

        public async Task<MyAssignedOnlineMeeting> GetMyAssignedOnlineMeeting()
        {
            return await HttpService.Get<MyAssignedOnlineMeeting>(Links.myAssignedOnlineMeeting);
        }

        public async Task<MyOnlineMeetings> GetMyOnlineMeetings()
        {
            return await HttpService.Get<MyOnlineMeetings>(Links.myOnlineMeetings);
        }

        public async Task<OnlineMeetingDefaultValues> GetOnlineMeetingDefaultValues()
        {
            return await HttpService.Get<OnlineMeetingDefaultValues>(Links.onlineMeetingDefaultValues);
        }

        public async Task<OnlineMeetingEligibleValues> GetOnlineMeetingEligibleValues()
        {
            return await HttpService.Get<OnlineMeetingEligibleValues>(Links.onlineMeetingEligibleValues);
        }

        public async Task<OnlineMeetingInvitationCustomization> GetOnlineMeetingInvitationCustomization()
        {
            return await HttpService.Get<OnlineMeetingInvitationCustomization>(Links.onlineMeetingInvitationCustomization);
        }

        public async Task<OnlineMeetingPolicies> GetOnlineMeetingPolicies()
        {
            return await HttpService.Get<OnlineMeetingPolicies>(Links.onlineMeetingPolicies);
        }

        public async Task<PhoneDialInInformation> GetPhoneDialInInformation()
        {
            return await HttpService.Get<PhoneDialInInformation>(Links.phoneDialInInformation);
        }
    }
}
