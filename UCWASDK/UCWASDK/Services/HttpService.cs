using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Services
{
    /// <summary>
    /// Handle all Http related calls to UCWA server.
    /// </summary>
    static public class HttpService
    {
        static public async Task<T> Get<T>(UCWAHref href) where T : UCWAModelBase
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Get<T>(href.Href);
        }

        static public async Task<List<T>> GetList<T>(UCWAHref[] hrefs) where T : UCWAModelBase
        {
            if (hrefs == null || hrefs.Count() == 0)
                return null;

            List<T> list = new List<T>();
            foreach (var href in hrefs) { if (!string.IsNullOrEmpty(href.Href)) list.Add(await Get<T>(href.Href)); }

            return list;
        }

        static public async Task<T> Get<T>(string uri) where T : UCWAModelBase
        {
            if(!uri.StartsWith("http"))
                uri = Settings.Host + uri;

            using (HttpClient client = await GetClient(uri))
            {   
                await Settings.UCWAClient.GetToken(client, uri);
                
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                    GetPGuid(jObject as JToken);
                    return JsonConvert.DeserializeObject<T>(jObject.ToString());
                }
                else
                    await HandleError(response);
            }

            return default(T);
        }

        static public async Task<byte[]> GetBinary(UCWAHref href)
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return null;

            var uri = href.Href;
            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;

            using (HttpClient client = await GetClient(uri))
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsByteArrayAsync();
                else
                    await HandleError(response);
            }

            return null;
        }

        static public async Task<string> Post(UCWAHref href, object body, string version = "")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return "";

            return await Post(href.Href, body, version);
        }

        static public async Task<string> Post(string uri, object body, string version = "")
        {
            HttpResponseMessage response = await PostInternal(uri, body, version);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Created)
                    return response.Headers.Location.ToString();
                else
                    return "";
            }
            else
                await HandleError(response);

            return "";
        }

        static public async Task<T> Post<T>(UCWAHref href, object body, string version = "")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Post<T>(href.Href, body);
        }

        static public async Task<T> Post<T>(string uri, object body, string version = "")
        {
            HttpResponseMessage response = await PostInternal(uri, body, version);

            if (response.IsSuccessStatusCode)
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            }
            else
                await HandleError(response);

            return default(T);
        }

        static public async Task Put(UCWAHref href, UCWAModelBase body, string version = "")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return;

            await Put(href.Href, body, version);
        }

        static public async Task Put(string uri, UCWAModelBase body, string version = "")
        {
            var response = await PutInternal(uri, body, version);
            if (response.IsSuccessStatusCode)
                return;
            else
                await HandleError(response);
        }

        static public async Task<T> Put<T>(UCWAHref href, UCWAModelBase body, string version = "")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Put<T>(href.Href, body, version);
        }

        static public async Task<T> Put<T>(string uri, UCWAModelBase body, string version = "")
        {            
            var response = await PutInternal(uri, body, version);
            if (response.IsSuccessStatusCode)
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            }
            else
                await HandleError(response);

            return default(T);
        }

        static public async Task Delete(UCWAHref href, string version = "")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return;

            await Delete(href.Href, version);
        }

        static public async Task Delete(string uri, string version = "")
        {
            if (string.IsNullOrEmpty(uri))
                return;

            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;

            using (HttpClient client = await GetClient(uri, version))
            {
                var response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    return;
                else
                    await HandleError(response);
            }
        }

        static private async Task<HttpResponseMessage> PostInternal(string uri, object body, string version = "")
        {
            if (string.IsNullOrEmpty(uri))
                return new HttpResponseMessage();

            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;

            if(body is UCWAModelBase)
            {
                JsonSerializer serializer = new JsonSerializer() { DefaultValueHandling = DefaultValueHandling.Ignore };
                serializer.Converters.Add(new StringEnumConverter());
                JObject jobject = JObject.FromObject(body, serializer);
                if(!(body is MessagingInvitation))
                    jobject["_links"]?.Parent?.Remove();
                jobject["_embedded"]?.Parent?.Remove();

                body = jobject;
            }

            using (HttpClient client = await GetClient(uri, version))
            {
                HttpResponseMessage response = null;

                if (body is string)
                    response = await client.PostAsync(uri, string.IsNullOrEmpty(body.ToString()) ? null : new StringContent(body.ToString(), Encoding.UTF8));
                else
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    settings.Converters.Add(new StringEnumConverter());
                    response = await client.PostAsync(uri, body == null ? null :
                        new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json"));
                }
                return response;
            }
        }

        static private async Task<HttpResponseMessage> PutInternal(string uri, UCWAModelBase body, string version = "")
        {
            if (string.IsNullOrEmpty(uri))
                return new HttpResponseMessage();

            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;

            using (HttpClient client = await GetClient(uri, version))
            {
                JsonSerializer serializer = new JsonSerializer() { DefaultValueHandling = DefaultValueHandling.Ignore };
                serializer.Converters.Add(new StringEnumConverter());
                JObject jobject = JObject.FromObject(body, serializer);
                jobject["_links"]?.Parent?.Remove();
                jobject["_embedded"]?.Parent?.Remove();
                jobject.Add(body.PGuid, "please pass this in a PUT request");
                client.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue("\"" + jobject["etag"].Value<string>() + "\""));

                return await client.PutAsync(uri, 
                     new StringContent(JsonConvert.SerializeObject(jobject, new StringEnumConverter()), Encoding.UTF8, "application/json"));
            }
        }

        static private async Task HandleError(HttpResponseMessage response)
        {
            var error = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    throw new Exception(GenericSynchronousError.ServiceFailure.ToString());
                case HttpStatusCode.NotFound:
                    if(response.RequestMessage.RequestUri.ToString() == Settings.Host + Settings.UCWAClient.Application.Self)
                        throw new ApplicationNotFoundException(error);
                    else
                        throw new ResourceNotFoundException(error);
                default:
                    throw new InvalidOperationException(error);
                    
            }
        }

        static private void GetPGuid(JToken jToken)
        {
            if (jToken is JArray)
                foreach (var jtoken in jToken as JArray) { GetPGuid(jtoken); }
            else
            {
                var pGuidObj = jToken.Values().Where(x => x.ToString() == "please pass this in a PUT request").FirstOrDefault();
                if (pGuidObj != null)
                    jToken["pGuid"] = (pGuidObj.Parent as JProperty).Name;
                if (jToken["_embedded"] != null)
                    foreach (var embedded in jToken["_embedded"]) { GetPGuid(embedded.First()); }
            }
        }

        static private async Task<HttpClient> GetClient(string uri, string version = "")
        {
            HttpClient client = new HttpClient();
            if(!string.IsNullOrEmpty(version))
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-MS-RequiresMinResourceVersion", "2");
            await Settings.UCWAClient.GetToken(client, uri);
            return client;
        }
    }
}
