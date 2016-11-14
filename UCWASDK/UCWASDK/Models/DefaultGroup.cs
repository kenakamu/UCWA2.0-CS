namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a persistent, system-created group where a user's contacts are placed by default. 
    /// An application can subscribe to updates from members of this group. Updates include presence, location, or note changes for a specific contact. Currently, defaultGroup is a read-only resource and can be managed by other endpoints. An application must call startOrRefreshSubscriptionToContactsAndGroups before it can receive events when a defaultGroup is created, modified, or removed.
    /// </summary>
    public class DefaultGroup : Group
    {
    }
}
