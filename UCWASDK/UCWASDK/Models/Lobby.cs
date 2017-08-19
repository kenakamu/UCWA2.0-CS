using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a view of the participants who have not yet been admitted to an onlineMeeting.
    /// A participant in the lobby can be admitted by another participant in the organizer or presenter role. A participant in the lobby who is not admitted will eventually be disconnected. Lobby settings are controlled by the meeting organizer using the accessLevel property of onlineMeeting. 
    /// </summary>
    public class Lobby : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("participant")]
            internal UCWAHref[] participant { get; set; }
        }

        public async Task<List<Participant>> GetParticipants()
        {
            return await HttpService.GetList<Participant>(Links.participant);
        }
    }
}
