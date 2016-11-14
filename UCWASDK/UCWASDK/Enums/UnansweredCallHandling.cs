namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents whether unanswered calls will be forwarded.
    /// </summary>
    public enum UnansweredCallHandling
    {
        /// <summary>
        /// Unanswered call handling is disabled. This is the case when CallForwardingSettingsResource.ActiveSetting is not CallForwardingState.ImmediateForward.
        /// </summary>
        Disabled,
        /// <summary>
        /// Unanswered call handling is enabled.
        /// </summary>
        Enabled
    }
}
