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
using System.Net;
using System.Xml.Linq;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a message transcript within a conversationLog.
    /// </summary>
    public class MessageTranscript : UCWAModelBase
    {
        [JsonIgnore]
        public string Text
        {
            get
            {
                if (Links.htmlMessage != null)
                    return XDocument.Parse(WebUtility.UrlDecode(Links.htmlMessage?.Href?.Split(',')[1])).Element("span").Value;
                else if (Links.plainMessage != null)
                    return WebUtility.UrlDecode(Links.plainMessage?.Href?.Split(',')[1]);
                else
                    return "";
            }
        }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("htmlMessage")]
            internal UCWAHref htmlMessage { get; set; }
     
            [JsonProperty("plainMessage")]
            internal UCWAHref plainMessage { get; set; }
        }        
    }
}
