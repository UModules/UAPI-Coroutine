using System;
using System.Collections.Generic;
using UAPIModule.Abstraction;
using UAPIModule.SharedTypes;
using UnityEngine;

namespace UAPIModule
{
    public static class APIClient
    {
        private static readonly Dictionary<string, RequestSender> requestSenders = new();

        public static void CreateRequest(string key, INetworkScreen networkScreen)
        {
            TryCreate(key, networkScreen, out _);
        }

        public static void SendRequest(string key, APIConfigData config, RequestFeedbackConfig feedbackConfig, RequestSendConfig sendConfig, Action<NetworkResponse> callback)
        {
            if (requestSenders.TryGetValue(key, out var requestSender))
            {
                requestSender.SendRequest(config, feedbackConfig, sendConfig, callback);
            }
            throw new KeyNotFoundException($"No request sender found with key '{key}'.");
        }

        public static void CreateAndSendRequest(string key, INetworkScreen networkScreen, APIConfigData config, RequestFeedbackConfig feedbackConfig, RequestSendConfig sendConfig, Action<NetworkResponse> callback)
        {
            if (!requestSenders.TryGetValue(key, out var requestSender) && !TryCreate(key, networkScreen, out requestSender))
            {
                callback(null);
                return;
            }
            requestSender.SendRequest(config, feedbackConfig, sendConfig, callback);
        }

        public static void SendRequest<K>(string key, APIConfigData config, RequestFeedbackConfig feedbackConfig, RequestSendConfig sendConfig, Action<NetworkResponse<K>> callback) where K : class
        {
            if (requestSenders.TryGetValue(key, out var requestSender))
            {
                requestSender.SendRequest(config, feedbackConfig, sendConfig, callback);
            }
            throw new KeyNotFoundException($"No request sender found with key '{key}'.");
        }

        public static void CreateAndSendRequest<K>(string key, INetworkScreen networkScreen, APIConfigData config, RequestFeedbackConfig feedbackConfig, RequestSendConfig sendConfig, Action<NetworkResponse<K>> callback) where K : class
        {
            if (!requestSenders.TryGetValue(key, out var requestSender) && !TryCreate(key, networkScreen, out requestSender))
            {
                callback(null);
                return;
            }
            requestSender.SendRequest(config, feedbackConfig, sendConfig, callback);
        }

        private static bool TryCreate(string key, INetworkScreen networkScreen, out RequestSender requestSender)
        {
            if (requestSenders.ContainsKey(key))
            {
                Debug.LogError($"RequestSender with key '{key}' already exists.");
                requestSender = null;
                return false;
            }

            requestSender = new RequestSender(networkScreen);
            requestSenders.Add(key, requestSender);
            return true;
        }
    }
}
