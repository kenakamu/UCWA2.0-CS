using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of phone resources that represent the phone numbers of the logged-on user. 
    /// This collection is read/write; it can be used to both create new phone resources, as well as to view and remove existing phone resources. 
    /// </summary>
    public class UCWAPhones : UCWAModelBaseLink
    {
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; } = new InternalEmbedded();

        [JsonIgnore]
        public Phone[] Phones { get { return Embedded.phones; } }

        internal class InternalEmbedded
        {
            [JsonProperty("phone")]
            internal Phone[] phones { get; set; } = new Phone[0];
        }    
    }
}
