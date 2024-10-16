using System;
using UAPIModule.SharedTypes;
using UnityEngine;

namespace UAPIModule.Tools
{
    internal class RequestLogger
    {
        public void LogRequest(string url)
        {
            Debug.Log($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Send -> Status: <color=yellow>Requested</color> -> {url}");
        }

        public void LogResponse<T>(NetworkResponse<T> response, string url) where T : class
        {
            if (response.isSuccessful)
                LogResponseSuccess(response.data);
            else
                LogResponseError(response);

            void LogResponseSuccess(T data)
            {
                Debug.Log($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Resp - Status: <color=green>Success</color> -> {url} Data -> {JsonUtility.ToJson(data)}");
            }

            void LogResponseError(NetworkResponse<T> response)
            {
                Debug.LogError($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Resp - Status: <color=red>Error</color> -> {url} Response Code -> {response.statusCode} Error -> {response.errorMessage}");
            }
        }

        public void LogResponse(NetworkResponse response, string url)
        {
            if (response.isSuccessful)
                LogResponseSuccess();
            else
                LogResponseError(response);

            void LogResponseSuccess()
            {
                Debug.Log($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Resp - Status: <color=green>Success</color> -> {url}");
            }

            void LogResponseError(NetworkResponse response)
            {
                Debug.LogError($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Resp - Status: <color=red>Error</color> -> {url} Response Code -> {response.statusCode} Error -> {response.errorMessage}");
            }
        }

        public void LogCustomMessage(string url, string message)
        {
            Debug.Log($"[HTTPRequest] SendRoutine ({DateTime.Now:T}) -> Resp - Status: <color=white>{message}</color> -> {url}");
        }
    }
}
