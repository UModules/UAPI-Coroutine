using System;
using UAPIModule.SharedTypes;

namespace UAPIModule
{
    public static class APIClient
    {
        private static readonly RequestSender requestSender;

        static APIClient()
        {
            requestSender = RequestSender.Instance;
        }

        public static void SendRequest(APIConfig config, RequestScreenConfig screenConfig, RequestSendConfig sendConfig, Action<NetworkResponse> callback)
        {
            requestSender.SendRequest(config, screenConfig, sendConfig, callback);
        }

        public static void SendRequest<K>(APIConfig config, RequestScreenConfig feedbackConfig, RequestSendConfig sendConfig, Action<NetworkResponse<K>> callback) where K : class
        {
            requestSender.SendRequest(config, feedbackConfig, sendConfig, callback);
        }
    }
}
