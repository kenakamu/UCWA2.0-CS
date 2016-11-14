namespace Microsoft.Skype.UCWA.Models
{

    /// <summary>
    /// Simultaneously send all incoming calls to a user's team, in addition to the user. 
    /// The presence of this resource indicates that the user can have her calls simultaneously ring her team as well as herself. The calls ring for the user as well as her team. The user can specify a delay between the time the call rings for herself and for her team. A team is a set of contacts that receive redirected calls as a unit. (See http://technet.microsoft.com/en-us/library/dd425271(v=office.13).aspx.) This version of the API does not support team management. 
    /// </summary>
    public class SimultaneousRingToTeam : UCWAModelBaseLink
    {
    }
}
