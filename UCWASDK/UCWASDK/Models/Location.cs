using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's location. 
    /// location gets updated whenever the user changes his or her location. 
    /// </summary>
    public class Location : UCWAModelBaseLink
    {
        [JsonProperty("location")]
        public string Address { get; set; }

        public Task Update()
        {
            return Update(HttpService.GetNewCancellationToken());
        }
        public async Task Update(CancellationToken cancellationToken)
        {
            await HttpService.Post(Self, this, cancellationToken);
        }
    }   
}
