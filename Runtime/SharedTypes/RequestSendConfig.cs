using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace UAPIModule
{
    public class RequestSendConfig
    {
        [JsonProperty] public Dictionary<string, object> RequestBody { get; set; } = null;
        public List<KeyValuePair<string, string>> RequestHeaders { get; set; } = null;
        public string AccessToken { get; set; } = null;

        internal bool HasBody => RequestBody != null && RequestBody.Any();
        internal bool HasHeaders => RequestHeaders != null && RequestHeaders.Any();
    }
}
