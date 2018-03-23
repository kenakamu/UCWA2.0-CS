using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the set of myOnlineMeetings currently on the user's calendar. 
    /// This resource can be used to create new myOnlineMeetings as well as to modify and delete existing ones. 
    /// </summary>
    public class MyOnlineMeetings : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; } = new InternalEmbedded();

        [JsonIgnore]
        public MyAssignedOnlineMeeting[] AssignedOnlineMeetings { get { return Embedded.assignedOnlineMeetings; } }

        [JsonIgnore]
        public MyOnlineMeeting[] OnlineMeetings { get { return Embedded.onlineMeetings; } }

        internal class InternalEmbedded
        {
            [JsonProperty("myAssignedOnlineMeeting")]
            internal MyAssignedOnlineMeeting[] assignedOnlineMeetings { get; set; } = new MyAssignedOnlineMeeting[0];

            [JsonProperty("myOnlineMeeting")]
            internal MyOnlineMeeting[] onlineMeetings { get; set; } = new MyOnlineMeeting[0];
        }

        public Task<MyOnlineMeeting> Create(MyOnlineMeeting meeting)
        {
            return Create(meeting, HttpService.GetNewCancellationToken());
        }
        public Task<MyOnlineMeeting> Create(MyOnlineMeeting meeting, CancellationToken cancellationToken)
        {
            return HttpService.Post<MyOnlineMeeting>(Self, meeting, cancellationToken);
        }
    }
}
