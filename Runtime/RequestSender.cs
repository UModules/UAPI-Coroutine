using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Threading;
using UAPIModule.SharedTypes;
using UAPIModule.Tools;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace UAPIModule
{
    internal class RequestSender : MonoBehaviour
    {
        protected static readonly HttpClient httpClient = new();
        internal readonly RequestLogger requestLogger = new();

        private static RequestSender _instance;

        public static RequestSender Instance
        {
            get
            {
                if (_instance == null)
                {
                    var runnerObject = new GameObject("RequestSender");
                    _instance = runnerObject.AddComponent<RequestSender>();
                    DontDestroyOnLoad(runnerObject);
                }
                return _instance;
            }
        }

        public void SendRequest<T>(APIRequestConfig config, RequestScreenConfig screenConfig, Action<NetworkResponse<T>> onComplete) where T : class
        {
            StartCoroutine(SendRequestCoroutine(config, screenConfig, onComplete));
        }

        public void SendRequest(APIRequestConfig config, RequestScreenConfig screenConfig, Action<NetworkResponse> onComplete)
        {
            StartCoroutine(SendRequestCoroutine(config, screenConfig, onComplete));
        }

        private IEnumerator SendRequestCoroutine<T>(APIRequestConfig config, RequestScreenConfig screenConfig, Action<NetworkResponse<T>> onComplete) where T : class
        {
            if (httpClient == null)
            {
                throw new InvalidOperationException("HttpClient is not initialized.");
            }

            var cancellationTokenSource = new CancellationTokenSource(config.Timeout);

            HttpResponseMessage response = null;

            yield return StartCoroutine(SendRequestInternal(config, screenConfig, cancellationTokenSource.Token, (result) => response = result));

            if (response != null)
            {
                var responseBodyTask = response.Content.ReadAsStringAsync();
                while (!responseBodyTask.IsCompleted)
                {
                    yield return null;
                }

                string responseBody = responseBodyTask.Result;

                var networkResponse = new NetworkResponse<T>
                {
                    isSuccessful = response.IsSuccessStatusCode,
                    statusCode = (long)response.StatusCode,
                    errorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase,
                    data = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(responseBody) : null
                };

                if (!networkResponse.isSuccessful)
                {
                    networkResponse.errorMessage = responseBody;
                }

                requestLogger.LogResponse(networkResponse, config.URL);
                ShowResponseMessage(networkResponse, screenConfig);

                onComplete?.Invoke(networkResponse);
            }
            else
            {
                onComplete?.Invoke(null);
            }
        }

        private IEnumerator SendRequestCoroutine(APIRequestConfig config, RequestScreenConfig screenConfig, Action<NetworkResponse> onComplete)
        {
            if (httpClient == null)
            {
                throw new InvalidOperationException("HttpClient is not initialized.");
            }

            var cancellationTokenSource = new CancellationTokenSource(config.Timeout);

            HttpResponseMessage response = null;

            yield return StartCoroutine(SendRequestInternal(config, screenConfig, cancellationTokenSource.Token, (result) => response = result));

            if (response != null)
            {
                var responseBodyTask = response.Content.ReadAsStringAsync();
                while (!responseBodyTask.IsCompleted)
                {
                    yield return null;
                }

                string responseBody = responseBodyTask.Result;

                var networkResponse = new NetworkResponse
                {
                    isSuccessful = response.IsSuccessStatusCode,
                    statusCode = (long)response.StatusCode,
                    errorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
                };

                if (!networkResponse.isSuccessful)
                {
                    networkResponse.errorMessage = responseBody;
                }

                requestLogger.LogResponse(networkResponse, config.URL);
                ShowResponseMessage(networkResponse, screenConfig);

                onComplete?.Invoke(networkResponse);
            }
            else
            {
                onComplete?.Invoke(null);
            }
        }

        private IEnumerator SendRequestInternal(APIRequestConfig config, RequestScreenConfig screenConfig, CancellationToken cancellationToken, Action<HttpResponseMessage> onComplete)
        {
            string url = config.URL;

            HttpRequestMessage requestMessage = new(new HttpMethod(config.MethodType.ToString()), url);
            AddHeaders(requestMessage, config);

            if ((config.MethodType == HTTPRequestMethod.POST
                || config.MethodType == HTTPRequestMethod.PUT
                || config.MethodType == HTTPRequestMethod.PATCH) && config.HasBody)
            {
                SetRequestBody(requestMessage, config);
            }

            requestLogger.LogRequest(url);

            screenConfig.TryShowScreen();

            var sendTask = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            bool completed = false;

            sendTask.ContinueWith(t => completed = true);

            while (!completed)
            {
                yield return null;
            }

            if (sendTask.IsCanceled || sendTask.IsFaulted)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    HandleCustomError(new TimeoutException("Request timed out"), config, screenConfig); // Handle timeout
                    throw new TimeoutException("Request timed out");
                }

                HandleCustomError(sendTask.Exception ?? new Exception("An error occurred during the request."), config, screenConfig); // Handle other errors
                throw sendTask.Exception ?? new Exception("An error occurred during the request.");
            }

            onComplete?.Invoke(sendTask.Result);
            screenConfig.TryHideScreen();
        }

        protected void AddHeaders(HttpRequestMessage requestMessage, APIRequestConfig config)
        {
            if (config.HeadersParameters != null)
            {
                foreach (var header in config.HeadersParameters)
                {
                    if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            if (config.NeedsAuthHeader)
            {
                string authToken = config.AccessToken;
                if (string.IsNullOrEmpty(authToken))
                {
                    Debug.LogError("Auth token is null or empty");
                    throw new InvalidOperationException("Auth token is null or empty");
                }
                requestMessage.Headers.Add("Authorization", authToken);
            }
        }

        protected void SetRequestBody(HttpRequestMessage requestMessage, APIRequestConfig config)
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(config.Bodies));

            var contentTypeHeader = config.HeadersParameters.FirstOrDefault(h => h.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)).Value;
            if (contentTypeHeader != null)
            {
                requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentTypeHeader);
            }
        }

        protected void ShowResponseMessage(NetworkResponse response, RequestScreenConfig screenConfig)
        {
            screenConfig.TryShowMessage(response);
        }

        protected void HandleCustomError(Exception exception, APIRequestConfig config, RequestScreenConfig screenConfig)
        {
            string errorMessage;
            long statusCode;

            if (exception is TimeoutException)
            {
                errorMessage = "The request timed out.";
                statusCode = (long)HTTPResponseCodes.REQUEST_TIMEOUT_408;
            }
            else if (exception is HttpRequestException httpRequestException)
            {
                errorMessage = GetErrorMessage(httpRequestException);
                statusCode = (long)HTTPResponseCodes.SERVER_ERROR_500;
            }
            else
            {
                errorMessage = exception.Message;
                statusCode = (long)HTTPResponseCodes.SERVER_ERROR_500;
            }

            var errorResponse = new NetworkResponse
            {
                isSuccessful = false,
                statusCode = statusCode,
                errorMessage = errorMessage
            };

            requestLogger.LogResponse(errorResponse, config.URL);
            ShowResponseMessage(errorResponse, screenConfig);
        }

        private string GetErrorMessage(HttpRequestException e)
        {
            string errorResponseBody = e.Message;
            if (e.Data.Contains("ResponseBody"))
            {
                errorResponseBody = e.Data["ResponseBody"].ToString();
            }
            return errorResponseBody;
        }
    }
}
