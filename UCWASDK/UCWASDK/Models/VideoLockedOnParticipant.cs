namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the participant on whom the video spotlight is locked in an onlineMeeting. 
    /// All participants in the onlineMeeting who are viewing video should see this participant's video feed. This resource helps an application keep track of the participant in the video spotlight for the corresponding onlineMeeting via the event channel.
    /// </summary>
    public class VideoLockedOnParticipant : Participant
    {
    }
}
