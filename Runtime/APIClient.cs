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

        public static void SendRequest(APIRequestConfig config, RequestScreenConfig screenConfig, Action<NetworkResponse> callback)
        {
            requestSender.SendRequest(config, screenConfig, callback);
        }

        public static void SendRequest<K>(APIRequestConfig config, RequestScreenConfig feedbackConfig, Action<NetworkResponse<K>> callback) where K : class
        {
            requestSender.SendRequest(config, feedbackConfig, callback);
        }
    }
}
