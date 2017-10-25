namespace Microsoft.Skype.UCWA.Enums
{
    /// <summary>
    /// Error Subcodes
    /// https://msdn.microsoft.com/en-us/skype/ucwa/errorsubcode_ref
    /// </summary>
    public enum ErrorSubCode
    {
        /// <summary>
        /// The resource you're trying to create already exists.
        /// </summary>
        AlreadyExists,
        /// <summary>
        /// This operation is not allowed for anonymous users
        /// </summary>
        AnonymousNotAllowed,
        /// <summary>
        /// Another operation is already in progress.
        /// </summary>
        AnotherOperationPending,
        /// <summary>
        /// Requested API version is not supported.
        /// </summary>
        APIVersionNotSupported,
        /// <summary>
        /// Application Not Found.
        /// </summary>
        ApplicationNotFound,
        /// <summary>
        /// Attendee is not allowed to perform operation, for demoted scenario.
        /// </summary>
        AttendeeNotAllowed,
        /// <summary>
        /// The invitation was auto-accepted.
        /// </summary>
        AutoAccepted,
        /// <summary>
        /// Remote side is busy and cannot fullfill request.
        /// </summary>
        Busy,
        /// <summary>
        /// CallbackUri supplied by the application could not be reached.
        /// </summary>
        CallbackUriUnreachable,
        /// <summary>
        /// Call is terminated.
        /// </summary>
        CallTerminated,
        /// <summary>
        /// Canceled.
        /// </summary>
        Canceled,
        /// <summary>
        /// Call is connected elsewhere.
        /// </summary>
        ConnectedElsewhere,
        /// <summary>
        /// The conversation does not exist.
        /// </summary>
        ConversationNotFound,
        /// <summary>
        /// CallDeclined.
        /// </summary>
        Declined,
        /// <summary>
        /// Participant was demoted.
        /// </summary>
        Demoted,
        /// <summary>
        /// The lobby participant was denied from the conference.
        /// </summary>
        Denied,
        /// <summary>
        /// Indicates that a new derived conversation is the reason for the event.
        /// </summary>
        DerivedConversation,
        /// <summary>
        /// The request body could not be deserialized.
        /// </summary>
        DeserializationFailure,
        /// <summary>
        /// The targeted user doesn't exist.
        /// </summary>
        DestinationNotFound,
        /// <summary>
        /// Dialout not allowed.
        /// </summary>
        DialoutNotAllowed,
        /// <summary>
        /// Do not disturb.
        /// </summary>
        DoNotDisturb,
        /// <summary>
        /// The user is removed from conference.
        /// </summary>
        Ejected,
        /// <summary>
        /// The conference has finished.
        /// </summary>
        Ended,
        /// <summary>
        /// Conference escalation failed.
        /// </summary>
        EscalationFailed,
        /// <summary>
        /// The Exchange request timed out
        /// </summary>
        ExchangeTimeout,
        /// <summary>
        /// The parked audiovideo session was terminated due to timeout.
        /// </summary>
        Expired,
        /// <summary>
        /// Federation is required.
        /// </summary>
        FederationRequired,
        /// <summary>
        /// Call Forwarded Locally
        /// </summary>
        Forwarded,
        /// <summary>
        /// Inactive application expired.
        /// </summary>
        InactiveApplicationExpired,
        /// <summary>
        /// Outgoing call establishment failed due to insufficient bandwidth.
        /// </summary>
        InsufficientBandwidth,
        /// <summary>
        /// The media description supplied is not valid.
        /// </summary>
        InvalidMediaDescription,
        /// <summary>
        /// Invitees only.
        /// </summary>
        InviteesOnly,
        /// <summary>
        /// Remote party doesn't support IPv6 IP addresses.
        /// </summary>
        IPv6NotSupported,
        /// <summary>
        /// A server defined limit was exceeded.
        /// </summary>
        LimitExceeded,
        /// <summary>
        /// Location Information Service is unavailable
        /// </summary>
        LisServiceUnavailable,
        /// <summary>
        /// The MakeMeAvailable operation is a prerequisite for this operation.
        /// </summary>
        MakeMeAvailableRequired,
        /// <summary>
        /// There was media encryption mismatch between local and remote clients.
        /// </summary>
        MediaEncryptionMismatch,
        /// <summary>
        /// Cannot support media encryption required by client.
        /// </summary>
        MediaEncryptionNotSupported,
        /// <summary>
        /// Media encryption is required to establish media.
        /// </summary>
        MediaEncryptionRequired,
        /// <summary>
        /// Media failure.
        /// </summary>
        MediaFailure,
        /// <summary>
        /// Incoming media is being handled by fallback logic.
        /// </summary>
        MediaFallback,
        /// <summary>
        /// Call was missed.
        /// </summary>
        Missed,
        /// <summary>
        /// Media negotiation request failed.
        /// </summary>
        MediaNegotiationFailure,
        /// <summary>
        /// Media negotiation request timed out.
        /// </summary>
        MediaNegotiationTimeOut,
        /// <summary>
        /// There is a migration in progress, contact's buddy list is read-only.
        /// </summary>
        MigrationInProgress,
        /// <summary>
        /// Modality not supported.
        /// </summary>
        ModalityNotSupported,
        /// <summary>
        /// Call was terminated because another presenter started sharing content.
        /// </summary>
        NewContentSharer,
        /// <summary>
        /// No delegates configured.
        /// </summary>
        NoDelegatesConfigured,
        /// <summary>
        /// Phone normalization failed.
        /// </summary>
        NormalizationFailed,
        /// <summary>
        /// LIS could not find a matched location
        /// </summary>
        NoLocationFound,
        /// <summary>
        /// The remote client is unable to accept the call at this time or context.
        /// </summary>
        NotAcceptable,
        /// <summary>
        /// The remote party is not allowed to process this request due to policy.
        /// </summary>
        NotAllowed,
        /// <summary>
        /// No team members configured.
        /// </summary>
        NoTeamMembersConfigured,
        /// <summary>
        /// The specified operation is not supported right now
        /// </summary>
        OperationNotSupported,
        /// <summary>
        /// The message content type was invalid.
        /// </summary>
        ParameterValidationFailure,
        /// <summary>
        /// The existing P-GET was replaced with a new P-GET from the client application.
        /// </summary>
        PGetReplaced,
        /// <summary>
        /// Invitation failed because multiple users are associated with the destination phone number.
        /// </summary>
        PhoneNumberConflict,
        /// <summary>
        ///Provisioning data could not be retrieved
        /// </summary>
        ProvisioningDataUnavailable,
        /// <summary>
        ///The attempt to reach mobile user's simulring number failed.
        /// </summary>
        PstnCallFailed,
        /// <summary>
        /// Indicates that the invitation was redirected to another participant.
        /// </summary>
        Redirected,
        /// <summary>
        /// The user was removed from the Mcu.
        /// </summary>
        Removed,
        /// <summary>
        /// The modality was replaced.
        /// </summary>
        Replaced,
        /// <summary>
        /// The recipient is responding in another way, such as IM or phone.
        /// </summary>
        RepliedWithOtherModality,
        /// <summary>
        /// Call's session has changed.
        /// </summary>
        SessionSwitched,
        /// <summary>
        /// Mcu shuts down.
        /// </summary>
        TemporarilyUnavailable,
        /// <summary>
        /// A conversation already exists with the same thread id.
        /// </summary>
        ThreadIdAlreadyExists,
        /// <summary>
        /// Service Timeout.
        /// </summary>
        Timeout,
        /// <summary>
        /// There are too many applications.
        /// </summary>
        TooManyApplications,
        /// <summary>
        /// There are too many contacts.
        /// </summary>
        TooManyContacts,
        /// <summary>
        /// User has exceeded max conversations.
        /// </summary>
        TooManyConversations,
        /// <summary>
        /// There are too many groups.
        /// </summary>
        TooManyGroups,
        /// <summary>
        /// Too many lobby participants.
        /// </summary>
        TooManyLobbyParticipants,
        /// <summary>
        /// The user has reached their online meeting limit.
        /// </summary>
        TooManyOnlineMeetings,
        /// <summary>
        /// Too many participants.
        /// </summary>
        TooManyParticipants,
        /// <summary>
        /// The call transfer reqeust is declined at transferee side.
        /// </summary>
        TransferDeclined,
        /// <summary>
        /// Call Transferred.
        /// </summary>
        Transferred,
        /// <summary>
        /// The call initiated from transferee gets declined at transfer target side.
        /// </summary>
        TransferTargetDeclined,
        /// <summary>
        /// No Mcu is available via the MCU factory.
        /// </summary>
        Unavailable,
        /// <summary>
        /// The target is not reachable.
        /// </summary>
        Unreachable,
        /// <summary>
        /// The content-type is not supported.
        /// </summary>
        UnsupportedMediaType,
        /// <summary>
        /// The user agent supplied is not allowed.
        /// </summary>
        UserAgentNotAllowed
    }
}