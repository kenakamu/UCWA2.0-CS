namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the participant in a conversation who is currently sharing a program or her screen.
    /// Today, the API does not support viewing this modality. However, this information can be useful for UI updates or for letting the user contact the sharer to let her know that he cannot see the content. The absence of this resource indicates that no one is sharing a program or their screen. 
    /// </summary>
    public class ApplicationSharer : Participant
    {
    }
}
