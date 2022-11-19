using BestServices.Core;
using BestServices.Model;
using BestServices.Model.DataBase.Commands;
using BestServices.Model.Messages;

namespace BestServices.ViewModel
{
    internal class RegVM : ObservableObject
    {
        public string Title => "Registration";

        private NewUser _newUser;
        public NewUser NewUser
        {
            get => _newUser;
            set => _newUser = value;
        }

        public RelayCommand ReturnCommand { get; private set; }
        public RelayCommand SignUpCommand { get; private set; }

        public RegVM()
        {
            NewUser = new NewUser();

            ReturnCommand = new RelayCommand(obj => App.Messenger.Send(new SetCurrentView(new AuthVM())));

            SignUpCommand = new RelayCommand(obj => Insert.InsertUser(ref _newUser));
        }
    }
}