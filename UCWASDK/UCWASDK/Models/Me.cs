using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
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
        public Task<Me> Get()
        {
            return Get(HttpService.GetNewCancellationToken());
        }

        public async Task<Me> Get(CancellationToken cancellationToken)
        {
            var uri = Self;
            return await HttpService.Get<Me>(uri, cancellationToken);
        }
        public Task<CallForwardingSettings> GetCallForwardingSettings()
        {
            return GetCallForwardingSettings(HttpService.GetNewCancellationToken());
        }
        public async Task<CallForwardingSettings> GetCallForwardingSettings(CancellationToken cancellationToken)
        {
            return await HttpService.Get<CallForwardingSettings>(Links.callForwardingSettings, cancellationToken);
        }
        public Task<Location> GetLocation()
        {
            return GetLocation(HttpService.GetNewCancellationToken());
        }
        public async Task<Location> GetLocation(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Location>(Links.location, cancellationToken);
        }
        public Task<Note> GetNote()
        {
            return GetNote(HttpService.GetNewCancellationToken());
        }
        public async Task<Note> GetNote(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Note>(Links.note, cancellationToken);
        }
        public Task<Phone[]> GetPhones()
        {
            return GetPhones(HttpService.GetNewCancellationToken());
        }
        public async Task<Phone[]> GetPhones(CancellationToken cancellationToken)
        {
            UCWAPhones ucwaPhones = await HttpService.Get<UCWAPhones>(Links.phones, cancellationToken);
            if (ucwaPhones == null)
                ucwaPhones = new UCWAPhones();
            return ucwaPhones.Phones;
        }
        public Task<byte[]> GetPhoto()
        {
            return GetPhoto(HttpService.GetNewCancellationToken());
        }
        public async Task<byte[]> GetPhoto(CancellationToken cancellationToken)
        {
            return await HttpService.GetBinary(Links.photo, cancellationToken);
        }
        public Task<Presence> GetPresence()
        {
            return GetPresence(HttpService.GetNewCancellationToken());
        }
        public async Task<Presence> GetPresence(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Presence>(Links.presence, cancellationToken);
        }

        public Task MakeMeAvailable(Availability availability, bool supportMessage, bool supportAudio, bool supportPlainText, bool supportHtmlFormat, string phoneNumber)
        {
            return MakeMeAvailable(availability, supportMessage, supportAudio, supportPlainText, supportHtmlFormat, phoneNumber, HttpService.GetNewCancellationToken());
        }
        public async Task MakeMeAvailable(Availability availability, bool supportMessage, bool supportAudio, bool supportPlainText, bool supportHtmlFormat, string phoneNumber, CancellationToken cancellationToken)
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

            JObject body = new JObject
            {
                ["SupportedModalities"] = supportedModalities,
                ["SupportedMessageFormats"] = supportedMessageFormats,
                ["SignInAs"] = availability.ToString(),
                ["phoneNumber"] = phoneNumber
            };

            await HttpService.Post(Links.makeMeAvailable, body, cancellationToken);
        }

        public Task ReportMyActivity()
        {
            return ReportMyActivity(HttpService.GetNewCancellationToken());
        }
        public async Task ReportMyActivity(CancellationToken cancellationToken)
        {
            await HttpService.Post(Links.reportMyActivity, "", cancellationToken);
        }
    }
}
