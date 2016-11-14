using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a view of the participants who have not yet been admitted to an onlineMeeting. 
    /// A participant in the lobby can be admitted by another participant in the organizer or presenter role. A participant in the lobby who is not admitted will eventually be disconnected. Lobby settings are controlled by the meeting organizer using the accessLevel property of onlineMeeting. 
    /// </summary>
    public class Lobby : UCWAModelBaseLink
    {       
        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public Participant[] Participants { get { return Embedded.participants; } }

        internal class InternalEmbedded
        {
            [JsonProperty("participant")]
            internal Participant[] participants { get; set; }
        }
    }
}
