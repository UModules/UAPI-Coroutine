using System.Collections.Generic;
using UAPIModule.Assets;

namespace UAPIModule.SharedTypes
{
    public struct APIConfig
    {
        public string BaseURL { get; private set; }
        public string Endpoint { get; private set; }
        public HTTPRequestMethod MethodType { get; private set; }
        public Dictionary<string, string> HeadersParameters { get; private set; }
        public bool NeedsAuthHeader { get; private set; }
        public int Timeout { get; private set; }
        public bool UseBearerPrefix { get; private set; }

        public APIConfig(
            BaseURLConfig baseURLConfig,
            string endpoint,
            HTTPRequestMethod methodType,
            HttpRequestParams headers,
            bool needsAuthHeader,
            int timeout,
            bool useBearerPrefix) : this(baseURLConfig.BaseURL,
                                         endpoint,
                                         methodType,
                                         headers.ToDictionary(),
                                         needsAuthHeader,
                                         timeout,
                                         useBearerPrefix)
        {
        }

        public APIConfig(
            string baseURL,
            string endpoint,
            HTTPRequestMethod methodType,
            Dictionary<string, string> headers,
            bool needsAuthHeader,
            int timeout,
            bool useBearerPrefix)
        {
            BaseURL = baseURL;
            Endpoint = endpoint;
            MethodType = methodType;
            HeadersParameters = headers ?? new Dictionary<string, string>();
            NeedsAuthHeader = needsAuthHeader;
            Timeout = timeout;
            UseBearerPrefix = useBearerPrefix;
        }
    }
}
