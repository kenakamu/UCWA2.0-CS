using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents settings that govern call forwarding. 
    /// This resource can be used to set up rules on how to route audio calls for call forwarding and simultaneous ring. 
    /// </summary>
    public class CallForwardingSettings : UCWAModelBase
    {
        [JsonProperty("activePeriod")]
        public ActivePeriod ActivePeriod { get; set; }

        [JsonProperty("activeSetting")]
        public CallForwardingState ActiveSetting { get; set; }

        [JsonProperty("unansweredCallHandling")]
        public UnansweredCallHandling UnansweredCallHandling { get; set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonProperty("_embedded")]
        internal InternalEmbedded Embedded { get; set; }

        [JsonIgnore]
        public ImmediateForwardSettings ImmediateForwardSettings { get { return Embedded.immediateForwardSettings; } }

        [JsonIgnore]
        public SimultaneousRingSettings SimultaneousRingSettings { get { return Embedded.simultaneousRingSettings; } }

        [JsonIgnore]
        public UnansweredCallSettings UnansweredCallSettings { get { return Embedded.unansweredCallSettings; } }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("turnOffCallForwarding")]
            internal TurnOffCallForwarding turnOffCallForwarding { get; set; }
        }

        internal class InternalEmbedded
        {
            [JsonProperty("immediateForwardSettings")]
            internal ImmediateForwardSettings immediateForwardSettings { get; set; }

            [JsonProperty("simultaneousRingSettings")]
            internal SimultaneousRingSettings simultaneousRingSettings { get; set; }

            [JsonProperty("unansweredCallSettings")]
            internal UnansweredCallSettings unansweredCallSettings { get; set; }
        }
        
        public Task TurnOffCallForwarding()
        {
            return TurnOffCallForwarding(HttpService.GetNewCancellationToken());
        }
        public async Task TurnOffCallForwarding(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.turnOffCallForwarding, "", cancellationToken);
        }
        public Task Update(ActivePeriod activePeriod, CallForwardingState activeSetting, UnansweredCallHandling unansweredCallHandling)
        {
            return Update(activePeriod, activeSetting, unansweredCallHandling, HttpService.GetNewCancellationToken());
        }
        public async Task Update(ActivePeriod activePeriod, CallForwardingState activeSetting, UnansweredCallHandling unansweredCallHandling, CancellationToken cancellationToken)
        {
            ActivePeriod = activePeriod;
            UnansweredCallHandling = unansweredCallHandling;
            ActiveSetting = activeSetting;
            await HttpService.Put(Self, this, cancellationToken);
        }
    }
}
