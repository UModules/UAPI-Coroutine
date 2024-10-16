using UAPIModule.Abstraction;
using UnityEngine;

namespace UAPIModule.Tools
{
    public static class NetworkLoadingHandlerCreator
    {
        private static SimpleLoadingHandler loadingHandler;

        public static INetworkScreen SampleScreen
        {
            get
            {
                if (loadingHandler == null)
                {
                    loadingHandler = new GameObject("SimpleLoadingHandler").AddComponent<SimpleLoadingHandler>();
                }
                return loadingHandler;
            }
        }

        internal class SimpleLoadingHandler : MonoBehaviour, INetworkScreen
        {
            public void Show()
            {
                Debug.Log("Loading started...");
            }

            public void Hide()
            {
                Debug.Log("Loading ended.");
            }

            public void ShowMessage(NetworkResponse response)
            {
                Debug.Log(response.ToString());
            }
        }
    }
}
