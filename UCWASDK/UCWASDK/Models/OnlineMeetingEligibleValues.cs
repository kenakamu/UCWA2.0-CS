using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the eligible values that the application can choose from when scheduling a myOnlineMeeting. 
    /// </summary>
    public class OnlineMeetingEligibleValues : UCWAModelBase
    {
        [JsonProperty("accessLevel")]
        public AccessLevel AccessLevel { get; internal set; }

        [JsonProperty("automaticLeaderAssignment")]
        public AutomaticLeaderAssignment AutomaticLeaderAssignment { get; internal set; }

        [JsonProperty("eligibleOnlineMeetingRels")]
        public OnlineMeetingRel[] EligibleOnlineMeetingRels { get; internal set; }

        [JsonProperty("entryExitAnnouncement")]
        public EntryExitAnnouncement[] EntryExitAnnouncement { get; internal set; }

        [JsonProperty("lobbyBypassForPhoneUsers")]
        public LobbyBypassForPhoneUsers[] LobbyBypassForPhoneUsers { get; set; }

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
    }
}
