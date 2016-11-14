namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Represents whether calls will be forwarded, and if forwarded, how calls are to be forwarded.
    /// </summary>
    public enum CallForwardingState
    {
        /// <summary>
        /// Calls will be forwarded immediately and will not ring your work number.
        /// </summary>
        ImmediateForward,
        /// <summary>
        /// Calls will ring your work number and will also ring another phone or person.
        /// </summary>
        SimultaneousRing
    }
}
