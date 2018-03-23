using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    public class Redirect : UCWAModelBase
    {   
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("user")]
            internal User user { get; set; }

            [JsonProperty("xframe")]
            internal Xframe xframe { get; set; }
        }

        public Task<User> GetUser()
        {
            return GetUser(HttpService.GetNewCancellationToken());
        }

        public  Task<User> GetUser(CancellationToken cancellationToken)
        {
            return HttpService.Get<User>(Links.user, cancellationToken);
        }

        public Task<Xframe> GetXframe()
        {
            return GetXframe(HttpService.GetNewCancellationToken());
        }

        public Task<Xframe> GetXframe(CancellationToken cancellationToken)
        {
            return HttpService.Get<Xframe>(Links.xframe, cancellationToken);
        }
    }
}
