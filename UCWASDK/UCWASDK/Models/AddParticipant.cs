namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Invites a contact to participate in a multiparty conversation. 
    /// When a conversation is in peer-to-peer mode, addParticipant will first escalate the conversation from two-party to conferencing, and will then invite the contact to the conversation. When a conversation is multiparty, addParticipant merely invites the contact to the conversation. 
    /// </summary>
    public class AddParticipant : UCWAModelBaseLink
    {
    }
}
