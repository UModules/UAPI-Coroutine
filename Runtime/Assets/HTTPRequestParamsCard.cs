using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UAPIModule.Assets
{
    [CreateAssetMenu(fileName = nameof(HttpRequestParams), menuName = "UAPIModule/" + nameof(HttpRequestParams))]
    public class HttpRequestParams : ScriptableObject
    {
        [field: SerializeField] public List<KeyValueItem> Parameters { private set; get; }

        public bool HasEmptyParams() =>
            Parameters.Exists(keyValueItems =>
            string.IsNullOrEmpty(keyValueItems.key) || string.IsNullOrEmpty(keyValueItems.value));

        public void AddParam(string key, string value)
        {
            if (Parameters.Exists(keyValueItems => keyValueItems.key == key)) return;
            Parameters.Add(new KeyValueItem() { key = key, value = value });
        }

        /// <summary>
        /// Converts the List of KeyValueItem into a Dictionary<string, string>.
        /// </summary>
        /// <returns>A Dictionary containing the key-value pairs from Parameters.</returns>
        public Dictionary<string, string> ToDictionary()
        {
            return Parameters.ToDictionary(item => item.key, item => item.value);
        }

        [ContextMenu("Add Default Header Params")]
        public void AddDefaultHeaderParams()
        {
            AddParam("Content-Type", "application/json");
            AddParam("Accept", "application/json");
        }

        [Serializable]
        public class KeyValueItem
        {
            public string key;
            public string value;
        }
    }
}
