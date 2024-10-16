using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UAPIModule.Tools;

namespace UAPIModule.SharedTypes
{
    public class APIRequestConfig
    {
        public string URL { get; private set; }
        public HTTPRequestMethod MethodType { get; private set; }
        public Dictionary<string, string> HeadersParameters { get; private set; }
        [JsonProperty] public Dictionary<string, object> Bodies { get; private set; }
        public bool NeedsAuthHeader { get; private set; }
        public string AccessToken { get; private set; }
        public int Timeout { get; private set; }

        internal bool HasBody => Bodies != null && Bodies.Any();
        internal bool HasHeader => HeadersParameters != null && HeadersParameters.Any();

        private APIRequestConfig(
            string baseURL,
            string endpoint,
            HTTPRequestMethod methodType,
            Dictionary<string, string> headers,
            Dictionary<string, object> bodies,
            bool needsAuthHeader,
            string accessToken,
            int timeout)
        {
            if (needsAuthHeader && string.IsNullOrEmpty(accessToken))
            {
                throw new System.ArgumentException($"'{nameof(accessToken)}' cannot be null or empty.", nameof(accessToken));
            }

            URL = string.IsNullOrEmpty(baseURL) ? endpoint : UrlUtility.Join(baseURL, endpoint);
            MethodType = methodType;
            HeadersParameters = headers;
            NeedsAuthHeader = needsAuthHeader;
            Timeout = timeout;
            Bodies = bodies;
            AccessToken = accessToken;
        }

        public static APIRequestConfig GetWithToken(string baseURL,
                                                    string endpoint,
                                                    HTTPRequestMethod methodType,
                                                    Dictionary<string, string> headers,
                                                    Dictionary<string, object> bodies,
                                                    string accessToken,
                                                    int timeout) =>
            new(baseURL, endpoint, methodType, headers, bodies, true, accessToken, timeout);

        public static APIRequestConfig GetWithoutToken(string baseURL,
                                                       string endpoint,
                                                       HTTPRequestMethod methodType,
                                                       Dictionary<string, string> headers,
                                                       Dictionary<string, object> bodies,
                                                       int timeout) =>
            new(baseURL, endpoint, methodType, headers, bodies, false, string.Empty, timeout);
    }
}
