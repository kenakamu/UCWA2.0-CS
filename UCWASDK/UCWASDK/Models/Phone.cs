using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents one of the user's phone numbers. 
    /// These phone numbers can be used as targets for a user's incoming calls or made visible as part of the user's contact card. 
    /// </summary>
    public class Phone : UCWAModelBase
    {
        [JsonProperty("includeInContactCard")]
        public bool IncludeInContactCard { get; internal set; }

        [JsonProperty("number")]
        public string Number { get; internal set; }

        [JsonProperty("type")]
        public string Type { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("changeNumber")]
            internal ChangeNumber changeNumber { get; set; }

            [JsonProperty("changeVisibility")]
            internal ChangeVisibility changeVisibility { get; set; }
        }

        public async Task ChangeNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return;
            
            await HttpService.Post($"{Links.changeNumber.Href}?number={number}", null);
        }

        public async Task ChangeVisibility(bool includeInContactCard)
        {
            var uri = includeInContactCard ? Links.changeVisibility.Href + "?includeInContactCard" : Links.changeVisibility.Href;
            
            await HttpService.Post(uri, "");
        }
    }
}
