namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Enumeration of different relationship levels.
    /// </summary>
    public enum PrivacyRelationshipLevel
    {
        /// <summary>
        /// Contact is blocked. Only email address and name are shared with blocked contacts.
        /// </summary>
        Blocked,
        /// <summary>
        /// Contact belongs to the same company.
        /// </summary>
        Colleagues,
        /// <summary>
        /// Contact is external to the company.
        /// </summary>
        External,
        /// <summary>
        /// Contact is a friend or a family member.
        /// </summary>
        FriendsAndFamily,
        /// <summary>
        /// Relationship level cannot be determined.
        /// </summary>
        Unknown,
        /// <summary>
        /// Contact belongs to the same team or same workgroup.
        /// </summary>
        Workgroup
    }
}
