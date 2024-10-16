namespace UAPIModule.Abstraction
{
    public interface INetworkScreen
    {
        void ShowLoading();
        void HideLoading();
        void ShowMessage(NetworkResponse response);
    }
}
