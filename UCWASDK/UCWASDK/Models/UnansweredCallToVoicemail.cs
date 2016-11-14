namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Forward all incoming calls to the user's voicemail if she does not respond. 
    /// The presence of this resource indicates that the user can forward her unanswered incoming calls to her voicemail. The calls will be sent to voicemail after the specified number of seconds. 
    /// </summary>
    public class UnansweredCallToVoicemail : UCWAModelBaseLink
    {
    }
}
