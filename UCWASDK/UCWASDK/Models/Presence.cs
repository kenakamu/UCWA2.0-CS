using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's availability and activity. 
    /// presence is updated when the user's availability or activity changes.The user can express her willingness to communicate by manually changing her presence.
    /// </summary>
    public class Presence : UCWAModelBaseLink
    {
        [JsonProperty("activity")]
        public string Acitvity { get; set; }

        [JsonProperty("availability")]
        public Availability Availability { get; set; }

        public async Task Update()
        {
            await Update(HttpService.GetNewCancellationToken());
        }

        public async Task Update(CancellationToken cancellationToken)
        {
            await HttpService.Post(Self, this, cancellationToken);
        }
    }
}
