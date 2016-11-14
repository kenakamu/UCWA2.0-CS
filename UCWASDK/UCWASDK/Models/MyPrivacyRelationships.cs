using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the various privacy relationships that the user maintains with his or her contacts. 
    /// </summary>
    public class MyPrivacyRelationships : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public MyPrivacyRelationship[] PrivacyRelationships { get { return Embedded.privacyRelationship; } }

        internal class InternalEmbedded
        {
            [JsonProperty("myPrivacyRelationship")]
            internal MyPrivacyRelationship[] privacyRelationship { get; set; }
        }
    }
}
