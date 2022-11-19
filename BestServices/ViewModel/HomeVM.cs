using BestServices.Core;
using BestServices.Model.Messages;

namespace BestServices.ViewModel
{
    internal class HomeVM : ObservableObject
    {
        public string Title => "Home";

        public RelayCommand GoAuth { get; private set; }
        public RelayCommand GoReg { get; private set; }

        public HomeVM()
        {
            GoAuth = new RelayCommand(obj =>  App.Messenger.Send(new SetCurrentView(new AuthVM())));
            GoReg = new RelayCommand(obj => App.Messenger.Send(new SetCurrentView(new RegVM())));
        }
    }
}