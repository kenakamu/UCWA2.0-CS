using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a view of the participants having the attendee role in an onlineMeeting. 
    /// </summary>
    public class Attendees : UCWAModelBase
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
        public Task<List<Participant>> GetParticipants()
        {
            return GetParticipants(HttpService.GetNewCancellationToken());
        }
        public async Task<List<Participant>> GetParticipants(CancellationToken cancellationToken)
        {
            return await HttpService.GetList<Participant>(Links.participant, cancellationToken);
        }
    }
}
