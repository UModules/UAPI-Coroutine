namespace UAPIModule.SharedTypes
{
    public class RequestFeedbackConfig
    {
        public bool ShowRetryDialog { get; set; } = true;
        public bool IsForce { get; set; } = false;
        public bool ShowLoading { get; set; } = true;

        public static RequestFeedbackConfig NoFeedback => new()
        {
            IsForce = false,
            ShowLoading = false,
            ShowRetryDialog = false
        };

        public static RequestFeedbackConfig InitializationFeedback => new()
        {
            IsForce = true,
            ShowLoading = false,
            ShowRetryDialog = true
        };
    }
}
