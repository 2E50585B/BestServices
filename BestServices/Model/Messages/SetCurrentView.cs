using BestServices.Core;

namespace BestServices.Model.Messages
{
    internal class SetCurrentView
    {
        public ObservableObject ViewModel { get; private set; }

        public SetCurrentView(ObservableObject viewModel)
        {
            ViewModel = viewModel;
            App.Messenger.Send(this);
        }
    }
}