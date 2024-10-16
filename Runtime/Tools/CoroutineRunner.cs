using UnityEngine;

namespace UAPIModule.Tools
{
    internal class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    var runnerObject = new GameObject("CoroutineRunner");
                    _instance = runnerObject.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(runnerObject); // Ensures the GameObject is not destroyed when loading new scenes
                }
                return _instance;
            }
        }
    }
}
