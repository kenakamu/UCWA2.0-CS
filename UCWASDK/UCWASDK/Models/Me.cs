using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the user. 
    /// The me resource will be updated whenever the application becomes ready for incoming calls and leaves lurker mode ( makeMeAvailable). Note that me will not be updated if any of its properties, such as emailAddresses or title, change while the application is active. 
    /// </summary>
    public class Me : UCWAModelBase
    {
        [JsonProperty("department")]
        public string Department { get; internal set; }

        [JsonProperty("emailAddresses")]
        public string[] EmailAddresses { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("uri")]
        public string Uri { get; internal set; }

        [JsonProperty("_links")]
        internal InternalLinks Links { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        internal class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }

            [JsonProperty("callForwardingSettings")]
            internal UCWAHref callForwardingSettings { get; set; }
            
            [JsonProperty("location")]
            internal UCWAHref location { get; set; }

            [JsonProperty("makeMeAvailable")]
            internal MakeMeAvailable makeMeAvailable { get; set; }

            [JsonProperty("note")]
            internal UCWAHref note { get; set; }

            [JsonProperty("phones")]
            internal UCWAHref phones { get; set; }

            [JsonProperty("photo")]
            internal UCWAHref photo { get; set; }

            [JsonProperty("presence")]
            internal UCWAHref presence { get; set; }
     
            [JsonProperty("reportMyActivity")]
            internal ReportMyActivity reportMyActivity { get; set; }
        }

        public async Task<Me> Get()
        {
            var uri = Self;
            return await HttpService.Get<Me>(uri);
        }

        public async Task<CallForwardingSettings> GetCallForwardingSettings()
        {
            return await HttpService.Get<CallForwardingSettings>(Links.callForwardingSettings);
        }

        public async Task<Location> GetLocation()
        {
            return await HttpService.Get<Location>(Links.location);
        }

        public async Task<Note> GetNote()
        {
            return await HttpService.Get<Note>(Links.note);
        }

        public async Task<Phone[]> GetPhones()
        {
            UCWAPhones ucwaPhones = await HttpService.Get<UCWAPhones>(Links.phones);
            if (ucwaPhones == null)
                ucwaPhones = new UCWAPhones();
            return ucwaPhones.Phones;
        }

        public async Task<byte[]> GetPhoto()
        {
            return await HttpService.GetBinary(Links.photo);
        }

        public async Task<Presence> GetPresencs()
        {
            return await HttpService.Get<Presence>(Links.presence);
        }

        public async Task MakeMeAvailable(Availability availability, bool supportMessage, bool supportAudio, bool supportPlainText, bool supportHtmlFormat, string phoneNumber)
        {
            JArray supportedModalities = new JArray();
            JArray supportedMessageFormats = new JArray();
            if (supportMessage)
                supportedModalities.Add(ModalityType.Messaging.ToString());
            if (supportAudio)
                supportedModalities.Add(ModalityType.PhoneAudio.ToString());
            if (supportPlainText)
                supportedMessageFormats.Add(MessageFormat.Plain.ToString());
            if (supportHtmlFormat)
                supportedMessageFormats.Add(MessageFormat.Html.ToString());

            JObject body = new JObject();
            body["SupportedModalities"] = supportedModalities;
            body["SupportedMessageFormats"] = supportedMessageFormats;
            body["SignInAs"] = availability.ToString();
            body["phoneNumber"] = phoneNumber;

            await HttpService.Post(Links.makeMeAvailable, body);
        }

        public async Task ReportMyActivity()
        {
            await HttpService.Post(Links.reportMyActivity, "");
        }
    }
}
