namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Forward all incoming calls to a user-provided number or contact if the user does not respond.
    /// The presence of this resource indicates that the user can forward her unanswered incoming calls to the number or contact of her choosing. The calls will ring at the target after the specified number of seconds
    /// </summary>
    public class UnansweredCallToContact : UCWAModelBaseLink
    {
    }
}
