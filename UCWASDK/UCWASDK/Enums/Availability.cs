namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents a contact's availability values.
    /// </summary>
    public enum Availability
    {
        /// <summary>
        /// Availability is not set. Internal use only.
        /// </summary>
        NotSet,
        /// <summary>
        /// Away
        /// </summary>
        Away,
        /// <summary>
        /// Be Right Back
        /// </summary>
        BeRightBack,
        /// <summary>
        /// Busy
        /// </summary>
        Busy,
        /// <summary>
        /// Do Not Disturb
        /// </summary>
        DoNotDisturb,
        /// <summary>
        /// Indicates that a user or contact is busy but has not been active on her device.
        /// </summary>
        IdleBusy,
        /// <summary>
        /// Indicates that a user or contact is available to communicate but has not been active on her device.
        /// </summary>
        IdleOnline,
        /// <summary>
        /// Offline.
        /// </summary>
        Offline,
        /// <summary>
        /// Available.
        /// </summary>
        Online,
        /// <summary>
        /// None.
        /// </summary>
        None,
        /// <summary>
        /// Indicate to Reset. Internal use only.
        /// </summary>
        Reset
    }
}
