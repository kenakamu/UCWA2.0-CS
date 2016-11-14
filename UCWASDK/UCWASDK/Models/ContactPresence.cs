using Microsoft.Skype.UCWA.Enums;
using Newtonsoft.Json;
using System;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a contact's availability and activity. 
    /// contactPresence is updated when a contact's availability or activity changes.
    /// </summary>
    public class ContactPresence : UCWAModelBaseLink
    {
        [JsonProperty("activity")]
        public string Activity { get; internal set; }

        [JsonProperty("availability")]
        public Availability Availability { get; internal set; }

        [JsonProperty("deviceType")]
        public ContactDeviceType DeviceType { get; internal set; }

        [JsonProperty("lastActive")]
        public DateTime LastActive { get; internal set; }
    }
}
