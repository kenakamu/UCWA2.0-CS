namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// An enumeration of the various subscription states.
    /// </summary>
    public enum SubscriptionState
    {
        /// <summary>
        /// The subscription is connected. In the Connected state clients receive all notifications about changes to the resources that are subscribed to.
        /// </summary>
        Connected,
        /// <summary>
        /// The subscription is connecting, and is not yet connected.
        /// </summary>
        Connecting,
        /// <summary>
        /// The subscription is disconnected. In the Disconnected state clients do not receive notifications about changes to the resources that were subscribed to.
        /// </summary>
        Disconnected,
        /// <summary>
        /// The subscription is about to be disconnected.
        /// </summary>
        Disconnecting
    }
}
