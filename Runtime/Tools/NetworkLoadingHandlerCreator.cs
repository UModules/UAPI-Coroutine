using UAPIModule.Abstraction;
using UnityEngine;

namespace UAPIModule.Tools
{
    internal class SimpleLoadingHandler : MonoBehaviour, INetworkScreen
    {
        public void ShowLoading()
        {
            Debug.Log("Loading started...");
            // Implement loading UI show logic
        }

        public void HideLoading()
        {
            Debug.Log("Loading ended.");
            // Implement loading UI hide logic
        }

        public void ShowMessage(NetworkResponse response)
        {
            Debug.Log(response.ToString());
        }
    }

    public static class NetworkLoadingHandlerCreator
    {
        public static INetworkScreen CreateAndGet() =>
            new GameObject("SimpleLoadingHandler").AddComponent<SimpleLoadingHandler>();
    }
}
