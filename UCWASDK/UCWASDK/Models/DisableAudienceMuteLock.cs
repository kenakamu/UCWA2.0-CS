namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Disables the forced mute of attendees in a conversation. 
    /// The presence of this resource indicates that the user has the ability to remove the forced mute from attendees. Using this ability will not actually unmute attendees; however, they will be able to unmute themselves. 
    /// </summary>
    public class DisableAudienceMuteLock : UCWAModelBaseLink
    {
    }
}
