namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the remote participant who accepted the invitation of the user. 
    /// This resource is present at the time an outgoing invitation successfully completes. The resource usually contains a contact that can be compared to the to in the same invitation to see if the intended destination and the recipient match.
    /// </summary>
    public class AcceptedByParticipant : Participant
    {       
    }
}
