namespace Microsoft.Skype.UCWA.Models
{

    /// <summary>
    /// Simultaneously send all incoming calls to a user's delegates in addition to the user. 
    /// The presence of this resource indicates that the user can have her calls simultaneously ring her delegates as well as herself. The calls ring for the user as well as her delegates. The user can specify a delay between the time the call rings for her and for her delegates. A delegate is a contact that has been given the responsibility to answer and make calls on behalf of the user. This version of the API does not support delegate management.
    /// </summary>
    public class SimultaneousRingToDelegates : UCWAModelBaseLink
    {
    }
}
