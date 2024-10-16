namespace UAPIModule.Abstraction
{
    public interface INetworkScreen
    {
        void Show();
        void Hide();
        void ShowMessage(NetworkResponse response);
    }
}
