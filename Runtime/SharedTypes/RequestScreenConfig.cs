using UAPIModule.Abstraction;
using UAPIModule.Tools;

namespace UAPIModule.SharedTypes
{
    public class RequestScreenConfig
    {
        private readonly bool ActiveScreen = true;
        private readonly INetworkScreen NetworkScreen;

        private RequestScreenConfig(bool activeScreen, INetworkScreen networkScreen)
        {
            ActiveScreen = activeScreen;
            NetworkScreen = networkScreen;
        }

        public static RequestScreenConfig GetNoScreen() => new(false, null);
        public static RequestScreenConfig GetDefaultScreen() => new(true, NetworkLoadingHandlerCreator.SampleScreen);
        public static RequestScreenConfig GetCustomScreen(INetworkScreen networkScreen) => new(true, networkScreen);

        public void TryShowScreen()
        {
            if (!ActiveScreen)
                return;

            NetworkScreen.Show();
        }

        public void TryHideScreen()
        {
            if (!ActiveScreen)
                return;

            NetworkScreen.Hide();
        }

        public bool TryShowMessage(NetworkResponse response)
        {
            if (!ActiveScreen)
                return false;

            NetworkScreen.ShowMessage(response);
            return true;
        }
    }
}
