using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user's personal or out-of-office note. 
    /// note aggregates the user's personal and out-of-office notes into one displayable string. The application can only set the personal note. The out-of-office note gets displayed when set in Exchange.
    /// </summary>
    public class Note : UCWAModelBaseLink
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public NoteType Type { get; set; }

        public async Task Update()
        {
            await HttpService.Post(Self, this);
        }
    }
}
