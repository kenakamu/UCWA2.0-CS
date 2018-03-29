using Microsoft.Skype.UCWA.Services;
using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents custom data for the associated onlineMeeting that can be used by an application.
    /// An onlineMeetingExtension resource can have zero or more optional programmer-defined properties that represent key-value pairs. An application can set and retrieve these properties at any time during the life of the onlineMeeting. Applications that understand a particular extension can use the data to enhance the user's meeting experience. 
    /// </summary>
    public class OnlineMeetingExtension : UCWAModelBaseLink
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public OnlineMeetingExtensionType Type { get; set; }

        public Task Update()
        {
            return Update(HttpService.GetNewCancellationToken());
        }
        public Task Update(CancellationToken cancellationToken)
        {
            return HttpService.Put(Self, this, cancellationToken);
        }

        public Task Delete()
        {
            return Delete(HttpService.GetNewCancellationToken());
        }
        public Task Delete(CancellationToken cancellationToken)
        {
            return HttpService.Delete(Self, cancellationToken);
        }
    }
}
