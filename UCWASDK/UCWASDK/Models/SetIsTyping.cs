namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Sets the user's typing status in a conversation. 
    /// The presence of this resource indicates that the user can broadcast her typing status. This resource will start a short timer on the server during which the user will show up in the typingParticipants for this conversation. If the resource is not used again within that time, the user will be removed from typingParticipants. 
    /// </summary>
    public class SetIsTyping : UCWAModelBaseLink
    {
    }
}
