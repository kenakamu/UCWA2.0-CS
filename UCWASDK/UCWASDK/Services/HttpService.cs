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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Services
{
    /// <summary>
    /// Handle all Http related calls to UCWA server.
    /// </summary>
    static public class HttpService
    {
        private const string defaultVersion = "2.0";
        // Store HttpClient per request Uri
        static ConcurrentDictionary<string, HttpClient> clientPool = new ConcurrentDictionary<string, HttpClient>();
        static HttpClient _anonymousHttpClient;
        static HttpClient AnonymousHttpClient
        {
            get
            {
                if (_anonymousHttpClient == null)
                {
                    _anonymousHttpClient = new HttpClient();
                    _anonymousHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                }
                return _anonymousHttpClient;
            }
        }
        static private ExceptionMappingService exceptionMappingService = new ExceptionMappingService();

        [Obsolete]
        static public Task<T> Get<T>(UCWAHref href, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            return Get<T>(href, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<T> Get<T>(UCWAHref href, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Get<T>(href.Href, cancellationToken, version, anonymous);
        }
        [Obsolete]
        static public Task<List<T>> GetList<T>(UCWAHref[] hrefs, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            return GetList<T>(hrefs, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<List<T>> GetList<T>(UCWAHref[] hrefs, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            if (hrefs == null || !hrefs.Any())
                return null;

            List<T> list = new List<T>();
            foreach (var href in hrefs) { if (!string.IsNullOrEmpty(href.Href)) list.Add(await Get<T>(href.Href, cancellationToken, version, anonymous)); }

            return list;
        }
        [Obsolete]
        static public Task<T> Get<T>(string uri, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            return Get<T>(uri, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<T> Get<T>(string uri, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false) where T : UCWAModelBase
        {
            uri = EnsureUriContainsHttp(uri);

            return await ExecuteHttpCallAndRetry((token) => GetInternal(uri, token, version, anonymous), async (response) =>
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            }, cancellationToken);
        }
        static public async Task<HttpResponseMessage> GetInternal(string uri, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            var client = await GetClient(uri, cancellationToken, version, anonymous);
            return await client.GetAsync(uri, cancellationToken);
        }
        [Obsolete]
        static public Task<byte[]> GetBinary(UCWAHref href, string version = defaultVersion, bool anonymous = false)
        {
            return GetBinary(href, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<byte[]> GetBinary(UCWAHref href, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return null;

            var uri = EnsureUriContainsHttp(href.Href);

            return await ExecuteHttpCallAndRetry((token) => GetInternal(uri, token, version, anonymous), async (response) =>
            {
                return await response.Content.ReadAsByteArrayAsync();
            }, cancellationToken);
        }
        [Obsolete]
        static public Task<string> Post(UCWAHref href, object body, string version = defaultVersion, bool anonymous = false)
        {
            return Post(href, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<string> Post(UCWAHref href, object body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return string.Empty;

            return await Post(href.Href, body, cancellationToken, version, anonymous);
        }
        [Obsolete]
        static public Task<string> Post(string uri, object body, string version = defaultVersion, bool anonymous = false)
        {
            return Post<string>(uri, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<string> Post(string uri, object body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            return await ExecuteHttpCallAndRetry((token) => PostInternal(uri, body, token, version, anonymous), (response) =>
            {
                if (response.StatusCode == HttpStatusCode.Created)
                    return response.Headers.Location.ToString();
                else
                    return string.Empty;
            }, cancellationToken);
        }
        [Obsolete]
        static public Task<T> Post<T>(UCWAHref href, object body, string version = defaultVersion, bool anonymous = false)
        {
            return Post<T>(href, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<T> Post<T>(UCWAHref href, object body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            if (href == null || string.IsNullOrEmpty(href.Href))
                return default(T);

            return await Post<T>(href.Href, body, cancellationToken, version, anonymous);
        }
        [Obsolete]
        static public Task<T> Post<T>(string uri, object body, string version = defaultVersion, bool anonymous = false)
        {
            return Post<T>(uri, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<T> Post<T>(string uri, object body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            return await ExecuteHttpCallAndRetry((token) => PostInternal(uri, body, token, version, anonymous), async (response) =>
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            }, cancellationToken);
        }
        [Obsolete]
        static public Task Put(string uri, UCWAModelBase body, string version = defaultVersion, bool anonymous = false)
        {
            return Put(uri, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task Put(string uri, UCWAModelBase body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            await ExecuteHttpCallAndRetry((token) => PutInternal(uri, body, token, version, anonymous), cancellationToken);
        }
        private static CancellationTokenSource cts = new CancellationTokenSource();
        static internal CancellationToken GetNewCancellationToken()
        {
            if (cts == null)
                cts = new CancellationTokenSource();
            return cts.Token;
        }
        [Obsolete]
        static public Task<T> Put<T>(string uri, UCWAModelBase body, string version = defaultVersion, bool anonymous = false)
        {
            return Put<T>(uri, body, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task<T> Put<T>(string uri, UCWAModelBase body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            return await ExecuteHttpCallAndRetry((token) => PutInternal(uri, body, token, version, anonymous), async (response) =>
            {
                var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                GetPGuid(jObject as JToken);
                return JsonConvert.DeserializeObject<T>(jObject.ToString());
            }, cancellationToken);
        }
        [Obsolete]
        static public Task Delete(string uri, string version = defaultVersion, bool anonymous = false)
        {
            return Delete(uri, GetNewCancellationToken(), version, anonymous);
        }
        static public async Task Delete(string uri, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            if (string.IsNullOrEmpty(uri))
                return;

            uri = EnsureUriContainsHttp(uri);

            await ExecuteHttpCallAndRetry((token) => DeleteInternal(uri, token, version, anonymous), cancellationToken);
        }
        static public async Task<HttpResponseMessage> DeleteInternal(string uri, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            var client = await GetClient(uri, cancellationToken, version, anonymous);
            return await client.DeleteAsync(uri, cancellationToken);
        }
        static public void DisposeHttpClients()
        {
            foreach (var client in clientPool.Values)
                client.Dispose();
            cts?.Dispose();
            cts = null;
            clientPool.Clear();
            _anonymousHttpClient?.Dispose();
            _anonymousHttpClient = null;
        }

        static private string EnsureUriContainsHttp(string uri)
        {
            if (!uri.StartsWith("http"))
                uri = Settings.Host + uri;
            return uri;
        }
        static private async Task<HttpResponseMessage> PostInternal(string uri, object body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
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

            var client = await GetClient(uri, cancellationToken, version, anonymous);
            HttpResponseMessage response = null;

            if (body is string)
                response = await client.PostAsync(uri, string.IsNullOrEmpty(body.ToString()) ? null : new StringContent(body.ToString(), Encoding.UTF8), cancellationToken);
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                settings.Converters.Add(new StringEnumConverter());
                response = await client.PostAsync(uri, body == null ? null :
                    new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json"), cancellationToken);
            }
            return response;
        }
        static private async Task<HttpResponseMessage> PutInternal(string uri, UCWAModelBase body, CancellationToken cancellationToken, string version = defaultVersion, bool anonymous = false)
        {
            if (string.IsNullOrEmpty(uri))
                return new HttpResponseMessage();

            uri = EnsureUriContainsHttp(uri);

            var client = await GetClient(uri, cancellationToken, version, anonymous);
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
            return await client.SendAsync(request, cancellationToken);
        }
        static private async Task ExecuteHttpCallAndRetry(Func<CancellationToken, Task<HttpResponseMessage>> httpRequest, CancellationToken cancellationToken, Action<HttpResponseMessage> deserializationHandler = null)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest(cancellationToken);
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
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
                catch (TaskCanceledException ex) when (ex.CancellationToken != cancellationToken) //request timeout
                {
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount) && !cancellationToken.IsCancellationRequested);

            if (lastException != null)
                throw lastException;

        }
        static private async Task<T> ExecuteHttpCallAndRetry<T>(Func<CancellationToken, Task<HttpResponseMessage>> httpRequest, Func<HttpResponseMessage, Task<T>> deserializationHandler, CancellationToken cancellationToken)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest(cancellationToken);
                    if (response.IsSuccessStatusCode)
                        return await deserializationHandler(response);
                    else
                        await HandleError(response);
                }
                catch (Exception ex) when (ex is IUCWAException && (ex as IUCWAException).IsTransient)
                {
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
                catch (TaskCanceledException ex) when (ex.CancellationToken != cancellationToken) //request timeout
                {
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount) && !cancellationToken.IsCancellationRequested);

            if (lastException != null)
                throw lastException;

            return default(T); // this line is never going to be executed because previous one always throws an error
        }
        static private async Task<T> ExecuteHttpCallAndRetry<T>(Func<CancellationToken, Task<HttpResponseMessage>> httpRequest, Func<HttpResponseMessage, T> deserializationHandler, CancellationToken cancellationToken)
        {
            var retryCount = 0U;
            Exception lastException = null;
            do
                try
                {
                    var response = await httpRequest(cancellationToken);
                    if (response.IsSuccessStatusCode)
                        return deserializationHandler(response);
                    else
                        await HandleError(response);
                }
                catch (Exception ex) when (ex is IUCWAException && (ex as IUCWAException).IsTransient)
                {
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
                catch (TaskCanceledException ex) when (ex.CancellationToken != cancellationToken) //request timeout
                {
                    lastException = await HandleAuthExpirationAndDelay(retryCount, ex, cancellationToken);
                    retryCount++;
                }
            while (Settings.UCWAClient.TransientErrorHandlingPolicy.ShouldRetry(retryCount) && !cancellationToken.IsCancellationRequested);

            if (lastException != null)
                throw lastException;

            return default(T); // this line is never going to be executed because previous one always throws an error
        }
        private static async Task<Exception> HandleAuthExpirationAndDelay(uint retryCount, Exception ex, CancellationToken cancellationToken)
        {
            if (ex is AuthenticationExpiredException)
                DisposeHttpClients();
            await Task.Delay(Settings.UCWAClient.TransientErrorHandlingPolicy.GetNextErrorWaitTimeInMs(retryCount), cancellationToken);
            return ex;// memorizing the last transient exception in case we still encounter it but run out of retries
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
                var pGuidObj = jToken.Values().FirstOrDefault(x => x.ToString() == "please pass this in a PUT request");
                if (pGuidObj != null)
                    jToken["pGuid"] = (pGuidObj.Parent as JProperty).Name;
                if (jToken["_embedded"] != null)
                    foreach (var embedded in jToken["_embedded"]) { GetPGuid(embedded.First()); }
            }
        }
        /// <summary>
        /// Returns same HttpClient instance per Uri hostname.
        /// </summary>
        static private async Task<HttpClient> GetClient(string uri, CancellationToken cancellationToken, string version, bool anonymous = false)
        {
            HttpClient client;
            var hostname = new Uri(uri).Host;
            if (anonymous)
                client = AnonymousHttpClient;
            else if (clientPool.ContainsKey(hostname))
                client = clientPool[hostname];
            else
            {
                // If we want to consider concurrency in the future, we may implement lock, but as this is client library, I just keep it simple at the moment.
                client = new HttpClient();
                AddResourcesVersionValidation(version, client);
                if (!clientPool.TryAdd(hostname, client))
                {
                    client.Dispose();//we failed adding the new one because of concurrency issues, diposing to avoid memory leaks
                    client = clientPool[hostname];// As the pool contains the key already, get the HttpClient from the pool.
                }
            }

            // Get Token everytime via ADAL
            if (!anonymous && client.DefaultRequestHeaders.Authorization == null)
                await Settings.UCWAClient.GetToken(client, uri, cancellationToken);
            return client;
        }
        private static void AddResourcesVersionValidation(string version, HttpClient client)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-MS-RequiresMinResourceVersion", version);
        }
    }
}
