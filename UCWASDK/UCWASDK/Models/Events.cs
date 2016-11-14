namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the event channel resource. 
    /// Each event in the event channel will have a link to the resource that produced the event. Optionally, the resource itself could also be embedded in the event channel. However, the application should handle events with or without embedded resource. If the resource is not embedded, the application can fetch the resource if needed. 
    /// </summary>
    public class Events : UCWAModelBaseLink
    {
    }
}
