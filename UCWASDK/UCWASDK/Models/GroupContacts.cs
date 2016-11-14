using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of contact resources that belong to a particular group resource. 
    /// </summary>
    public class GroupContacts : UCWAModelBaseLink
    {
        [JsonProperty("property")]
        public string Property { get; internal set; }
                
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Contact[] Contacts { get { return Embedded.contacts; } }

        internal class InternalEmbedded
        {
            [JsonProperty("contact")]
            internal Contact[] contacts { get; set; }               
        }       
    }
}
