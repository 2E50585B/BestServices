using BestServices.Core;

namespace BestServices.ViewModel.Dialogs
{
    internal class SimpleNotificationVM : ObservableObject
    {
        public string Capture { get; private set; } = "Capture";
        public string Message { get; private set; } = "Message";

        public SimpleNotificationVM(string capture, string message)
        {
            Capture = capture;
            Message = message;
        }
    }
}