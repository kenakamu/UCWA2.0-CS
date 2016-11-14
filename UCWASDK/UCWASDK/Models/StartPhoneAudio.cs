namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Initiates a call-via-work. 
    /// startPhoneAudio allows a user to create a new peer-to-peer conversation with a contact using call-via-work. A call-via-work call is an outgoing call that is initiated from a user's phone, and that displays the user's Skype for Business work identity to the remote party. A phoneAudioInvitation will be started; its status can be tracked on the event channel.
    /// </summary>
    public class StartPhoneAudio : UCWAModelBaseLink
    {
    }
}
