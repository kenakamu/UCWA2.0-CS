namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Declines an incoming invitation. 
    /// This resource is used to decline an incoming messagingInvitation, phoneAudioInvitation or onlineMeetingInvitation as part of a new or existing conversation. decline causes the corresponding invitation to fail with an indication that it was declined. For an incoming messagingInvitation, decline will make the instant messaging modality disconnected in the corresponding conversation. For an incoming phoneAudioInvitation, decline will make the phone audio modality disconnected in the corresponding conversation. For an incoming onlineMeetingInvitation, decline will terminate the conversation. Other participants may remain active in the onlineMeeting. 
    /// </summary>
    public class Decline : UCWAModelBaseLink
    {
    }
}
