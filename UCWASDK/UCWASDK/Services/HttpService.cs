using Microsoft.Skype.UCWA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        // Store HttpClient per request Uri
        static ConcurrentDictionary<string, HttpClient> clientPool = new ConcurrentDictionary<string, HttpClient>();
        static private ExceptionMappingService exceptionMappingService = new ExceptionMappingService();
        
        static public async Task<T> Get<T>(UCWAHref href, string version = "2.0") where T : UCWAModelBase
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Get<T>(href.Href, version);
        }
        static public async Task<List<T>> GetList<T>(UCWAHref[] hrefs, string version = "2.0") where T : UCWAModelBase
        {
            if (hrefs == null || hrefs.Count() == 0)
                return null;

            List<T> list = new List<T>();
            foreach (var href in hrefs) { if (!string.IsNullOrEmpty(href.Href)) list.Add(await Get<T>(href.Href, version)); }

            return list;
        }
        static public async Task<T> Get<T>(string uri, string version = "2.0") where T : UCWAModelBase
        {
            uri = EnsureUriContainsHttp(uri);

            var client = await GetClient(uri, version);
            return await ExecuteHttpCallAndRetry(() => client.GetAsync(uri), async (response) =>
                {
                    var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                    GetPGuid(jObject as JToken);
                    return JsonConvert.DeserializeObject<T>(jObject.ToString());
                });
        }
        static public async Task<byte[]> GetBinary(UCWAHref href, string version = "2.0")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return null;

            var uri = EnsureUriContainsHttp(href.Href);

            var client = await GetClient(uri, version);
            return await ExecuteHttpCallAndRetry(() => client.GetAsync(uri), async (response) =>
            {
                return await response.Content.ReadAsByteArrayAsync();
            });
        }
        static public async Task<string> Post(UCWAHref href, object body, string version = "2.0")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return string.Empty;

            return await Post(href.Href, body, version);
        }
        static public async Task<string> Post(string uri, object body, string version = "2.0")
        {
            return await ExecuteHttpCallAndRetry(() => PostInternal(uri, body, version), (response) =>
            {
                if (response.StatusCode == HttpStatusCode.Created)
                    return response.Headers.Location.ToString();
                else
                    return string.Empty;
            });
        }
        static public async Task<T> Post<T>(UCWAHref href, object body, string version = "2.0")
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Post<T>(href.Href, body, version);
        }
        static public async Task<T> Post<T>(string uri, object body, string version = "2.0")
        {
            return await ExecuteHttpCallAndRetry(() => PostInternal(uri, body, version), async (response) =>
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            });
        }
        static public async Task Put(string uri, UCWAModelBase body, string version = "2.0")
        {
            await ExecuteHttpCallAndRetry(() => PutInternal(uri, body, version));
        }
        static public async Task<T> Put<T>(string uri, UCWAModelBase body, string version = "2.0")
        {
            return await ExecuteHttpCallAndRetry(() => PutInternal(uri, body, version), async (response) =>
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            });
        }
        static public async Task Delete(string uri, string version = "2.0")
        {
            if (string.IsNullOrEmpty(uri))
                return;

            uri = EnsureUriContainsHttp(uri);

            var client = await GetClient(uri, version);
            await ExecuteHttpCallAndRetry(() => client.DeleteAsync(uri));
        }
        static public void DisposeHttpClients()
        {
            foreach (var client in clientPool.Values)
            {
                client.Dispose();
            }
        }

        static private string EnsureUriContainsHttp(string uri)
        {
            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;
            return uri;
        }
        static private async Task<HttpResponseMessage> PostInternal(string uri, object body, string version = "")
        {
            if (string.IsNullOrEmpty(uri))
                return new HttpResponseMessage();

            uri = EnsureUriContainsHttp(uri);

            if (body is UCWAModelBase)
            {
                JsonSerializer serializer = new JsonSerializer() { DefaultValueHandling = DefaultValueHandling.Ignore };
                serializer.Converters.Add(new StringEnumConverter());
                JObject jobject = JObject.FromObject(body, serializer);
                if (!(body is MessagingInvitation))
                    jobject["_links"]?.Parent?.Remove();
                jobject["_embedded"]?.Parent?.Remove();

                body = jobject;
            }

            var client = await GetClient(uri, version);
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
        static private async Task<HttpResponseMessage> PutInternal(string uri, UCWAModelBase body, string version = "")
        {
            if (string.IsNullOrEmpty(uri))
                return new HttpResponseMessage();

            uri = EnsureUriContainsHttp(uri);

            var client = await GetClient(uri, version);
            JsonSerializer serializer = new JsonSerializer() { DefaultValueHandling = DefaultValueHandling.Ignore };
            serializer.Converters.Add(new StringEnumConverter());
            JObject jobject = JObject.FromObject(body, serializer);
            jobject["_links"]?.Parent?.Remove();
            jobject["_embedded"]?.Parent?.Remove();
            jobject.Add(body.PGuid, "please pass this in a PUT request");

            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(jobject, new StringEnumConverter()), Encoding.UTF8, "application/json")
            };
            request.Headers.Add("If-Match", "\"" + body.ETag + "\"");
            return await client.SendAsync(request);
        }
        static private async Task ExecuteHttpCallAndRetry(Func<Task<HttpResponseMessage>> httpRequest, Action<HttpResponseMessage> deserializationHandler = null)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest();
                    if (response.IsSuccessStatusCode)
                    {
                        deserializationHandler?.Invoke(response);
                        return;
                    }
                    else
                        await HandleError(response);
                }
                catch (Exception ex) when (ex is IUCWAException && (ex as IUCWAException).IsTransient)
                {
                    lastException = ex;// memorizing the last transient exception in case we still encounter it but run out of retries
                    await Task.Delay(Settings.UCWAClient.TransientErrorHandlingPolicy.GetNextErrorWaitTimeInMs(retryCount));
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount));

            if (lastException != null)
                throw lastException;

        }
        static private async Task<T> ExecuteHttpCallAndRetry<T>(Func<Task<HttpResponseMessage>> httpRequest, Func<HttpResponseMessage, Task<T>> deserializationHandler)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest();
                    if (response.IsSuccessStatusCode)
                        return await deserializationHandler(response);
                    else
                        await HandleError(response);
                }
                catch (Exception ex) when (ex is IUCWAException && (ex as IUCWAException).IsTransient)
                {
                    lastException = ex;// memorizing the last transient exception in case we still encounter it but run out of retries
                    await Task.Delay(Settings.UCWAClient.TransientErrorHandlingPolicy.GetNextErrorWaitTimeInMs(retryCount));
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount));

            if (lastException != null)
                throw lastException;

            return default(T); // this line is never going to be executed because previous one always throws an error
        }
        static private async Task<T> ExecuteHttpCallAndRetry<T>(Func<Task<HttpResponseMessage>> httpRequest, Func<HttpResponseMessage, T> deserializationHandler)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest();
                    if (response.IsSuccessStatusCode)
                        return deserializationHandler(response);
                    else
                        await HandleError(response);
                }
                catch (Exception ex) when (ex is IUCWAException && (ex as IUCWAException).IsTransient)
                {
                    lastException = ex;// memorizing the last transient exception in case we still encounter it but run out of retries
                    await Task.Delay(Settings.UCWAClient.TransientErrorHandlingPolicy.GetNextErrorWaitTimeInMs(retryCount));
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount));

            if (lastException != null)
                throw lastException;

            return default(T); // this line is never going to be executed because previous one always throws an error
        }
        static private async Task HandleError(HttpResponseMessage response)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw exceptionMappingService.GetExceptionFromHttpStatusCode(response, error);
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
        /// <summary>
        /// Returns same HttpClient instance per Uri hostname.
        /// </summary>
        static private async Task<HttpClient> GetClient(string uri, string version)
        {
            HttpClient client;
            var hostname = new Uri(uri).Host;
            if (clientPool.ContainsKey(hostname))
            {
                client = clientPool[hostname];
            }
            else
            {
                // If we want to consider concurrency in the future, we may implement lock, but as this is client library, I just keep it simple at the moment.
                client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-MS-RequiresMinResourceVersion", version);
                try
                {
                    if (!clientPool.TryAdd(hostname, client))
                    {
                        // As the pool contains the key already, get the HttpClient from the pool.
                        client = clientPool[hostname];
                    }
                }
                catch (OverflowException)
                {
                    // The dictionary already contains the maximum number of elements(MaxValue)
                    throw;
                }
                catch(Exception)
                {
                    throw;
                }
            }

            // Get Token everytime via ADAL
            await Settings.UCWAClient.GetToken(client, uri);
            return client;
        }
    }
}
