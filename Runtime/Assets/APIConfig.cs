using UAPIModule.SharedTypes;
using UnityEngine;

namespace UAPIModule.Assets
{
    [CreateAssetMenu(fileName = nameof(APIConfig), menuName = "UAPIModule/" + nameof(APIConfig), order = 1)]
    public class APIConfig : ScriptableObject
    {
        [SerializeField, Tooltip("The base URL configuration for the API request.")]
        private BaseURLConfig baseURLConfig;

        [SerializeField, Tooltip("The endpoint of the API request.")]
        private string endpoint;

        [SerializeField, Tooltip("The HTTP method type (GET, POST, PUT, etc.) for the API request.")]
        private HTTPRequestMethod methodType;

        [SerializeField, Tooltip("The headers for the API request.")]
        private HttpRequestParams headers;

        [SerializeField, Tooltip("Indicates whether the API request needs an authorization header.")]
        private bool needsAuthHeader;

        [SerializeField, Tooltip("The timeout duration for the API request in milliseconds.")]
        private int timeout = 1000;

        [SerializeField, Tooltip("Indicates whether to use the 'Bearer' prefix in the authorization header.")]
        private bool useBearerPrefix;

        public APIConfigData Get() =>
            new(baseURLConfig, endpoint, methodType, headers, needsAuthHeader, timeout, useBearerPrefix);
    }
}
