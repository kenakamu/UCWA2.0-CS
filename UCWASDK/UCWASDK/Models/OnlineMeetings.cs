using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
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

        public Task<MyAssignedOnlineMeeting> GetMyAssignedOnlineMeeting()
        {
            return GetMyAssignedOnlineMeeting(HttpService.GetNewCancellationToken());
        }
        public Task<MyAssignedOnlineMeeting> GetMyAssignedOnlineMeeting(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyAssignedOnlineMeeting>(Links.myAssignedOnlineMeeting, cancellationToken);
        }

        public Task<MyOnlineMeetings> GetMyOnlineMeetings()
        {
            return GetMyOnlineMeetings(HttpService.GetNewCancellationToken());
        }
        public Task<MyOnlineMeetings> GetMyOnlineMeetings(CancellationToken cancellationToken)
        {
            return HttpService.Get<MyOnlineMeetings>(Links.myOnlineMeetings, cancellationToken);
        }

        public Task<OnlineMeetingDefaultValues> GetOnlineMeetingDefaultValues()
        {
            return GetOnlineMeetingDefaultValues(HttpService.GetNewCancellationToken());
        }
        public Task<OnlineMeetingDefaultValues> GetOnlineMeetingDefaultValues(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnlineMeetingDefaultValues>(Links.onlineMeetingDefaultValues, cancellationToken);
        }

        public Task<OnlineMeetingEligibleValues> GetOnlineMeetingEligibleValues()
        {
            return GetOnlineMeetingEligibleValues(HttpService.GetNewCancellationToken());
        }
        public Task<OnlineMeetingEligibleValues> GetOnlineMeetingEligibleValues(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnlineMeetingEligibleValues>(Links.onlineMeetingEligibleValues, cancellationToken);
        }

        public Task<OnlineMeetingInvitationCustomization> GetOnlineMeetingInvitationCustomization()
        {
            return GetOnlineMeetingInvitationCustomization(HttpService.GetNewCancellationToken());
        }
        public Task<OnlineMeetingInvitationCustomization> GetOnlineMeetingInvitationCustomization(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnlineMeetingInvitationCustomization>(Links.onlineMeetingInvitationCustomization, cancellationToken);
        }

        public Task<OnlineMeetingPolicies> GetOnlineMeetingPolicies()
        {
            return GetOnlineMeetingPolicies(HttpService.GetNewCancellationToken());
        }
        public Task<OnlineMeetingPolicies> GetOnlineMeetingPolicies(CancellationToken cancellationToken)
        {
            return HttpService.Get<OnlineMeetingPolicies>(Links.onlineMeetingPolicies, cancellationToken);
        }

        public Task<PhoneDialInInformation> GetPhoneDialInInformation()
        {
            return GetPhoneDialInInformation(HttpService.GetNewCancellationToken());
        }
        public Task<PhoneDialInInformation> GetPhoneDialInInformation(CancellationToken cancellationToken)
        {
            return HttpService.Get<PhoneDialInInformation>(Links.phoneDialInInformation, cancellationToken);
        }
    }
}
