namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Immediately forward all incoming calls to the user's delegates.
    /// The presence of this resource indicates that the user can forward her incoming calls to her delegates. The calls will ring only for the delegates. A delegate is a contact that has been given the responsibility to answer and make calls on behalf of the user. This version of the API does not support delegate management.
    /// </summary>
    public class ImmediateForwardToDelegates : UCWAModelBaseLink
    {
    }
}
