using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Microsoft.Skype.UCWA.Services
{
    internal class ExceptionMappingService
    {
        private const string authenticationHeaderKey = "WWW-Authenticate";
        private const string matchKeyKey = "key";
        private const string matchValueKey = "value";
        private const string trustedIssuersKey = "trusted_issuers";
        private const string clientIdKey = "client_id";
        private const string grantTypeKey = "grant_type";
        private const string oAuthKey = "href";
        private static Regex authenticationMatchingRegex = new Regex($@"(?<{matchKeyKey}>[\w]+)=""(?<{matchValueKey}>[\w -@\.:/,] +)""");
        private Dictionary<string, string> getAuthenticationHeaderValues(HttpResponseHeaders headers)
        {
            var value = new Dictionary<string, string>();
            foreach (var authenticateHeaderValue in headers.GetValues(authenticationHeaderKey))
            {
                var match = authenticationMatchingRegex.Match(authenticateHeaderValue);
                while (match.Success)
                {
                    value.Add(match.Groups[matchKeyKey].Value, match.Groups[matchValueKey].Value);
                    match = match.NextMatch();
                }
            }
            return value;
        }
        internal Exception GetExceptionFromHttpStatusCode(HttpResponseMessage response, string error)
        {// we can start by guessing what kind of error it is by relying on the protocol first https://ucwa.skype.com/documentation/ProgrammingConcepts-Errors
            var reason = TryDeserializeReason(error);
            #region nonStandardHttpErrorCodes
            switch ((int)response.StatusCode)
            {
                case 428:
                    return new PreconditionFailedException(error) { Reason = reason, IsTransient = false };
                case 429: //TooManyRequests
                    return new GenericUCWAException(error) { Reason = reason, IsTransient = true };
            }
            #endregion
            switch (response.StatusCode)
            {
                #region transientErrors
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.Gone:
                case HttpStatusCode.RequestTimeout:
                case HttpStatusCode.InternalServerError:
                    // these ones should retry
                    return GetExceptionFromErrorCode(reason) ?? new GenericUCWAException() { Reason = reason, IsTransient = true };
                #endregion
                #region conflicts
                case HttpStatusCode.Conflict:
                case HttpStatusCode.ExpectationFailed:
                case HttpStatusCode.PreconditionFailed:
                    // these ones should try to handle the conflict
                    return new PreconditionFailedException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                #endregion
                #region nontransient
                case HttpStatusCode.Unauthorized:
                    if (response.Headers.Contains(authenticationHeaderKey))
                    {
                        var authenticationHeaderValues = getAuthenticationHeaderValues(response.Headers);
                        return new AuthenticationExpiredException(reason?.Message ?? error)
                        {
                            Reason = reason,
                            IsTransient = true,
                            ClientId = authenticationHeaderValues[clientIdKey],
                            GrantType = authenticationHeaderValues[grantTypeKey],
                            OAuthUri = authenticationHeaderValues[oAuthKey],
                            TrustedIssuers = authenticationHeaderValues[trustedIssuersKey]
                        };
                    }
                    else
                        return new UnauthorizedAccessException(error);
                case HttpStatusCode.Forbidden:
                    return new UnauthorizedAccessException(error);
                case HttpStatusCode.RequestEntityTooLarge:
                    return new EntityTooLargeException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                case HttpStatusCode.BadRequest:
                    return new BadRequestException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                case HttpStatusCode.MethodNotAllowed:
                    return new MethodNotAllowedException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                case HttpStatusCode.UnsupportedMediaType:
                    return new UnsupportedMediaTypeException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                case HttpStatusCode.NotFound:
                    if (response.RequestMessage.RequestUri.ToString() == Settings.Host + Settings.UCWAClient.Application.Self)
                        return new ApplicationNotFoundException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                    else
                        return new ResourceNotFoundException(reason?.Message ?? error) { Reason = reason, IsTransient = false };
                #endregion
                default:
                    return new InvalidOperationException(error);
            }
        }
        private Exception GetExceptionFromErrorCode(Reason reason)
        {
            if (reason == null)
                return new GenericUCWAException() { IsTransient = false };
            else
                switch (reason.Code)
                {// the mapping between generic exceptions and error code is obvious in some cases https://msdn.microsoft.com/en-us/skype/ucwa/genericsynchronouserrors https://msdn.microsoft.com/en-us/skype/ucwa/genericsynchronouserrors otherwise we rely on subcode

                    case ErrorCode.GatewayTimeout:
                        return new ServiceTimeoutException(reason.Message) { Reason = reason, IsTransient = true };
                    case ErrorCode.EntityTooLarge:
                        return new EntityTooLargeException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.Gone:
                        return new TooManyApplicationsException(reason.Message) { Reason = reason, IsTransient = true };
                    case ErrorCode.MethodNotAllowed:
                        return new MethodNotAllowedException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.NotFound:
                        return new ResourceNotFoundException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.PreconditionFailed:
                        return new PreconditionFailedException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.PreconditionRequired:
                        return new PreconditionRequiredException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.ServiceFailure:
                        return new ServiceFailureException(reason.Message) { Reason = reason, IsTransient = true };
                    case ErrorCode.Timeout:
                        return new ServiceTimeoutException(reason.Message) { Reason = reason, IsTransient = true };
                    case ErrorCode.UnsupportedMediaType:
                        return new UnsupportedMediaTypeException(reason.Message) { Reason = reason, IsTransient = false };
                    case ErrorCode.ExchangeServiceFailure:
                        return new ExchangeServiceFailureException(reason.Message) { Reason = reason, IsTransient = true };
                    case ErrorCode.NotAcceptable:
                    case ErrorCode.Forbidden:
                    case ErrorCode.BadRequest:
                    case ErrorCode.Conflict:
                    case ErrorCode.LocalFailure:
                    case ErrorCode.Informational:
                    case ErrorCode.ServiceUnavailable:
                    case ErrorCode.RemoteFailure:
                    case ErrorCode.TooManyRecepient:
                        return GetExceptionFromSubCode(reason);
                    default:
                        return new GenericUCWAException() { IsTransient = false };
                }
        }
        internal Reason TryDeserializeReason(string serializedReason)
        {
            try
            {
                if (string.IsNullOrEmpty(serializedReason))
                    return null;
                else
                    return JsonConvert.DeserializeObject<Reason>(serializedReason);
            }
            catch (Exception) // Error trying to deserialize the reason
            {//TODO: this should be logged because it's nevralgic to the error handling behavior. Not being .net standard doesn't help having a proper Trace API
                return null;
            }
        }
        private Exception GetExceptionFromSubCode(Reason reason)
        {// deterministic classification approach to map codes to exceptions and guess if its transient or not, not perfect but can be tweeked over time
            switch (reason.Subcode)
            {
                case ErrorSubCode.NotAllowed:
                case ErrorSubCode.InviteesOnly:
                case ErrorSubCode.DialoutNotAllowed:
                case ErrorSubCode.Denied:
                case ErrorSubCode.AttendeeNotAllowed:
                case ErrorSubCode.AnonymousNotAllowed:
                    return new UnauthorizedAccessException(reason.ToString());
                case ErrorSubCode.InsufficientBandwidth:
                case ErrorSubCode.AnotherOperationPending:
                    return new GenericUCWAException(reason.Message) { Reason = reason, IsTransient = true };
                case ErrorSubCode.APIVersionNotSupported:
                    return new VersionNotSupportedException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.ApplicationNotFound:
                    return new ApplicationNotFoundException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.Timeout:
                case ErrorSubCode.MediaNegotiationTimeOut:
                case ErrorSubCode.Busy:
                    return new ServiceTimeoutException(reason.Message) { Reason = reason, IsTransient = true };
                case ErrorSubCode.NoLocationFound:
                case ErrorSubCode.DestinationNotFound:
                case ErrorSubCode.ConversationNotFound:
                case ErrorSubCode.CallbackUriUnreachable:
                    return new ResourceNotFoundException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.RepliedWithOtherModality:
                case ErrorSubCode.TooManyApplications:
                case ErrorSubCode.ConnectedElsewhere:
                    return new TooManyApplicationsException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.NormalizationFailed:
                case ErrorSubCode.DeserializationFailure:
                    return new DeserializationFailureException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.LimitExceeded:
                case ErrorSubCode.ProvisioningDataUnavailable:
                case ErrorSubCode.PstnCallFailed:
                case ErrorSubCode.TemporarilyUnavailable:
                case ErrorSubCode.Unavailable:
                case ErrorSubCode.LisServiceUnavailable:
                case ErrorSubCode.MigrationInProgress:
                case ErrorSubCode.EscalationFailed:
                case ErrorSubCode.Unreachable:
                    return new ServiceFailureException(reason.Message) { Reason = reason, IsTransient = true };
                case ErrorSubCode.ExchangeTimeout:
                    return new ExchangeServiceFailureException(reason.Message) { Reason = reason, IsTransient = true };
                case ErrorSubCode.InactiveApplicationExpired:
                case ErrorSubCode.Expired:
                    return new InactiveApplicationExpiredException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.IPv6NotSupported:
                case ErrorSubCode.ModalityNotSupported:
                case ErrorSubCode.InvalidMediaDescription:
                    return new BadRequestException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.UnsupportedMediaType:
                case ErrorSubCode.MediaNegotiationFailure:
                case ErrorSubCode.MediaEncryptionRequired:
                case ErrorSubCode.MediaFailure:
                case ErrorSubCode.MediaFallback:
                case ErrorSubCode.MediaEncryptionMismatch:
                case ErrorSubCode.MediaEncryptionNotSupported:
                case ErrorSubCode.OperationNotSupported:
                    return new UnsupportedMediaTypeException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.NoDelegatesConfigured:
                    throw new ServiceFailureException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.ParameterValidationFailure:
                    throw new ParameterValidationFailureException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.NotAcceptable:
                case ErrorSubCode.UserAgentNotAllowed:
                    throw new MethodNotAllowedException(reason.Message) { Reason = reason, IsTransient = false };
                case ErrorSubCode.NewContentSharer:
                case ErrorSubCode.Replaced:
                case ErrorSubCode.SessionSwitched:
                case ErrorSubCode.PGetReplaced:
                case ErrorSubCode.ThreadIdAlreadyExists:
                case ErrorSubCode.TooManyContacts:
                case ErrorSubCode.TooManyConversations:
                case ErrorSubCode.TooManyGroups:
                case ErrorSubCode.TooManyLobbyParticipants:
                case ErrorSubCode.TooManyOnlineMeetings:
                case ErrorSubCode.TooManyParticipants:
                case ErrorSubCode.Removed:
                case ErrorSubCode.PhoneNumberConflict:
                case ErrorSubCode.Redirected:
                case ErrorSubCode.TransferDeclined:
                case ErrorSubCode.Transferred:
                case ErrorSubCode.TransferTargetDeclined:
                case ErrorSubCode.NoTeamMembersConfigured:
                case ErrorSubCode.Missed:
                case ErrorSubCode.MakeMeAvailableRequired:
                case ErrorSubCode.Forwarded:
                case ErrorSubCode.FederationRequired:
                case ErrorSubCode.Ejected:
                case ErrorSubCode.Ended:
                case ErrorSubCode.DoNotDisturb:
                case ErrorSubCode.DerivedConversation:
                case ErrorSubCode.Demoted:
                case ErrorSubCode.Declined:
                case ErrorSubCode.Canceled:
                case ErrorSubCode.CallTerminated:
                case ErrorSubCode.AutoAccepted:
                case ErrorSubCode.AlreadyExists:
                default:
                    return new GenericUCWAException(reason.Message) { Reason = reason, IsTransient = false };
            }
        }
    }
}
