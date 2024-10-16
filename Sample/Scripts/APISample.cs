using UAPIModule.SharedTypes;
using UnityEngine;

namespace UAPIModule.Sample
{
    public class APISample : MonoBehaviour
    {
        // GET Request
        [ContextMenu("GET Request")]
        private void OnGetRequest()
        {
            Debug.Log("Sending GET request...");
            APIConfig config = GetConfig("/get", HTTPRequestMethod.GET);

            APIClient.SendRequest<GetResponse>(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse<GetResponse> response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"GET Response: Origin - {response.data.origin}, URL - {response.data.url}");
                }
                else
                {
                    Debug.LogError("GET request failed: " + response.errorMessage);
                }
            }
        }

        // POST Request
        [ContextMenu("POST Request")]
        private void OnPostRequest()
        {
            Debug.Log("Sending POST request...");
            APIConfig config = GetConfig("/post", HTTPRequestMethod.POST);

            APIClient.SendRequest<PostResponse>(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse<PostResponse> response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"POST Response: Form Name - {response.data.form.name}");
                }
                else
                {
                    Debug.LogError("POST request failed: " + response.errorMessage);
                }
            }
        }

        // PUT Request
        [ContextMenu("PUT Request")]
        private void OnPutRequest()
        {
            Debug.Log("Sending PUT request...");
            APIConfig config = GetConfig("/put", HTTPRequestMethod.PUT);

            APIClient.SendRequest<PutResponse>(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse<PutResponse> response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"PUT Response: Data - {response.data.data}");
                }
                else
                {
                    Debug.LogError("PUT request failed: " + response.errorMessage);
                }
            }
        }

        // DELETE Request
        [ContextMenu("DELETE Request")]
        private void OnDeleteRequest()
        {
            Debug.Log("Sending DELETE request...");
            APIConfig config = GetConfig("/delete", HTTPRequestMethod.DELETE);

            APIClient.SendRequest<DeleteResponse>(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse<DeleteResponse> response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"DELETE Response: Origin - {response.data.origin}");
                }
                else
                {
                    Debug.LogError("DELETE request failed: " + response.errorMessage);
                }
            }
        }

        // HEAD Request
        [ContextMenu("HEAD Request")]
        private void OnHeadRequest()
        {
            Debug.Log("Sending HEAD request...");
            APIConfig config = GetConfig("/headers", HTTPRequestMethod.HEAD);

            APIClient.SendRequest(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"HEAD Request: Headers Received - {response.ToString()}");
                }
                else
                {
                    Debug.LogError("HEAD request failed: " + response.errorMessage);
                }
            }
        }

        // PATCH Request
        [ContextMenu("PATCH Request")]
        private void OnPatchRequest()
        {
            Debug.Log("Sending PATCH request...");
            APIConfig config = GetConfig("/patch", HTTPRequestMethod.PATCH);

            APIClient.SendRequest<PatchResponse>(config, RequestScreenConfig.GetDefaultScreen(), new(), Callback);

            void Callback(NetworkResponse<PatchResponse> response)
            {
                if (response.isSuccessful)
                {
                    Debug.Log($"PATCH Response: JSON Data - {response.data.json.name}");
                }
                else
                {
                    Debug.LogError("PATCH request failed: " + response.errorMessage);
                }
            }
        }

        // Custom APIConfigData method for each API call
        private APIConfig GetConfig(string endpoint, HTTPRequestMethod methodType)
        {
            return new APIConfig(
                "https://httpbin.org",                    // Base URL for httpbin
                endpoint,                                 // API endpoint specific to each method
                methodType,                               // HTTP method (GET, POST, etc.)
                null,                                     // Headers (empty for now, can be customized)
                false,                                    // NeedsAuthHeader (set false for this example)
                10000,                                       // Timeout in seconds
                false                                     // UseBearerPrefix
            );
        }

        // Response classes for each type of request
        [System.Serializable]
        public class GetResponse
        {
            public string origin;
            public string url;
        }

        [System.Serializable]
        public class PostResponse
        {
            public Form form;
            [System.Serializable]
            public class Form
            {
                public string name;
            }
        }

        [System.Serializable]
        public class PutResponse
        {
            public string data;
        }

        [System.Serializable]
        public class DeleteResponse
        {
            public string origin;
        }

        [System.Serializable]
        public class PatchResponse
        {
            public Json json;
            [System.Serializable]
            public class Json
            {
                public string name;
            }
        }

        // Create GUI buttons for testing
        private void OnGUI()
        {
            if (GUILayout.Button("Send GET Request"))
            {
                OnGetRequest();
            }
            if (GUILayout.Button("Send POST Request"))
            {
                OnPostRequest();
            }
            if (GUILayout.Button("Send PUT Request"))
            {
                OnPutRequest();
            }
            if (GUILayout.Button("Send DELETE Request"))
            {
                OnDeleteRequest();
            }
            if (GUILayout.Button("Send HEAD Request"))
            {
                OnHeadRequest();
            }
            if (GUILayout.Button("Send PATCH Request"))
            {
                OnPatchRequest();
            }
        }
    }
}
