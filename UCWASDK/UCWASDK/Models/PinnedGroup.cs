namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a system-created group of contacts that the user pins or that the user frequently communicates and collaborates with. 
    /// An application can subscribe to updates from members of this group. Updates include presence, location, or note changes for a specific contact. Currently, pinnedGroup is a read-only resource and can be managed by other endpoints. An application must call startOrRefreshSubscriptionToContactsAndGroups before it can receive events when a pinnedGroup is created, modified, or removed.
    /// </summary>
    public class PinnedGroup : Group
    {
    }
}
