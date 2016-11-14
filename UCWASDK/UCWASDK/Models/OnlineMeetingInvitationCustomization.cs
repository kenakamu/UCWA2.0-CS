using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the recommended custom values to use when an onlineMeetingInvitation is sent.
    /// </summary>
    public class OnlineMeetingInvitationCustomization : UCWAModelBaseLink
    {
        [JsonProperty("enterpriseHelpUrl")]
        public string EnterpriseHelpUrl { get; internal set; }

        [JsonProperty("invitationFooterText")]
        public string InvitationFooterText { get; internal set; }

        [JsonProperty("invitationHelpUrl")]
        public string InvitationHelpUrl { get; internal set; }

        [JsonProperty("invitationLegalUrl")]
        public string InvitationLegalUrl { get; internal set; }

        [JsonProperty("invitationLogoUrl")]
        public string InvitationLogoUrl { get; internal set; }
    }
}
