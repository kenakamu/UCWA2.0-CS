using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a contact's personal or out-of-office note. 
    /// contactNote aggregates the user's personal and out-of-office notes into one displayable string. The out-of-office note gets displayed when set in Exchange.
    /// </summary>
    public class ContactNote : UCWAModelBaseLink
    {
        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("type")]
        public NoteType Type { get; internal set; }
    }
}
