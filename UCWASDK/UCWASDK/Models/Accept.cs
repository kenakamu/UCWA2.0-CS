namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Accepts an incoming invitation. 
    /// This resource is used to accept an incoming messagingInvitation or onlineMeetingInvitation as part of a new or existing conversation. An application can determine whether the invitation was successfully accepted based on whether the corresponding invitation completed with success. For an incoming messagingInvitation, accept will change the status of the instant messaging modality to "Connected" in the corresponding conversation. Subsequently the application will be able to send and receive messages and typing notifications.For an incoming onlineMeetingInvitation, accept causes the user to join the corresponding onlineMeeting. The conversation state will change to "Conferenced". Note that the application is then responsible for joining a modality.
    /// </summary>
    public class Accept : UCWAModelBaseLink
    {
    }
}
