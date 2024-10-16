using UnityEngine;

namespace UAPIModule.Assets
{
    [CreateAssetMenu(fileName = nameof(BaseURLConfig), menuName = "UAPIModule/" + nameof(BaseURLConfig))]
    public class BaseURLConfig : ScriptableObject
    {
        [field: SerializeField] public string BaseURL { get; private set; }

        public static BaseURLConfig Create(string baseURL)
        {
            return new BaseURLConfig { BaseURL = baseURL };
        }
    }
}
