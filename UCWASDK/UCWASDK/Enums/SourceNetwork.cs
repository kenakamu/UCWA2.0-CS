namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// An enumeration that represents the source-network type of a participant.
    /// The participant's source network can be the same enterprise as the user's (SameEnterprise), an organization that is federated with the user's organization (Federated), or the public internet cloud (PublicCloud). The Everyone value includes SameEnterprise, Federated, and PublicCloud.
    /// </summary>
    public enum SourceNetwork
    {
        /// <summary>
        /// The contact source network includes PublicCloud, Federated, and SameEnterprise.
        /// </summary>
        Everyone,
        /// <summary>
        /// The source network is Federated.
        /// </summary>
        Federated,
        /// <summary>
        /// The source network is PublicCloud.
        /// </summary>
        PublicCloud,
        /// <summary>
        /// The source network is SameEnterprise.
        /// </summary>
        SameEnterprise,
        /// <summary>
        /// The source network is unknown.
        /// </summary>
        Unknown
    }
}
