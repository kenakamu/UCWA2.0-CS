using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the admin policies that can apply to a user's application. 
    /// Policies include information such as whether emoticons are allowed in messages or photos are enabled for contacts in the user's organization. Note that policies are set by the admin; they cannot be changed by the user. 
    /// </summary>
    public class Policies : UCWAModelBaseLink
    {
        [JsonProperty("customerExperienceImprovementProgram")]
        public string CustomerExperienceImprovementProgram { get; internal set; }

        [JsonProperty("emergencyDialMask")]
        public string EmergencyDialMask { get; internal set; }

        [JsonProperty("emergencyDialString")]
        public string EmergencyDialString { get; internal set; }

        [JsonProperty("emoticons")]
        public string Emoticons { get; internal set; }

        [JsonProperty("clientExchangeConnectivity")]
        public string ClientExchangeConnectivity { get; internal set; }

        [JsonProperty("exchangeUnifiedMessaging")]
        public string ExchangeUnifiedMessaging { get; internal set; }

        [JsonProperty("htmlMessaging")]
        public string HtmlMessaging { get; internal set; }

        [JsonProperty("logging")]
        public string Logging { get; internal set; }

        [JsonProperty("loggingLevel")]
        public string LoggingLevel { get; internal set; }

        [JsonProperty("messageArchiving")]
        public string MessageArchiving { get; internal set; }

        [JsonProperty("messagingUrls")]
        public string MessagingUrls { get; internal set; }

        [JsonProperty("photos")]
        public string Photos { get; internal set; }

        [JsonProperty("saveCallLogs")]
        public string SaveCallLogs { get; internal set; }

        [JsonProperty("saveCredentials")]
        public string SaveCredentials { get; internal set; }

        [JsonProperty("saveMessagingHistory")]
        public string SaveMessagingHistory { get; internal set; }

        [JsonProperty("sharingOnlyOnWifi")]
        public string SharingOnlyOnWifi { get; internal set; }

        [JsonProperty("telephonyMode")]
        public string TelephonyMode { get; internal set; }

        [JsonProperty("voicemailUri")]
        public string VoicemailUri { get; internal set; }
    }
}
