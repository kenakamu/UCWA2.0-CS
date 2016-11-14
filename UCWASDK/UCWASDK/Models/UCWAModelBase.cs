using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    public class UCWAHref
    {
        [JsonProperty("href")]
        public string Href { get; set; }


        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class UCWAModelBase : UCWAHref
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("etag")]
        public string ETag { get; set; }
        
        public string PGuid { get; set; }
        
    }
        
    public class UCWAModelBaseLink : UCWAModelBase
    {
        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        public string Self { get { return Links?.self?.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }
        }
    }   
}
