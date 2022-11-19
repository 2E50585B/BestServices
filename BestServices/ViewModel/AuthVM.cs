using System.Windows;
using BestServices.Core;
using BestServices.Model.DataBase;
using BestServices.Model.DataBase.Commands;
using BestServices.Model.Messages;
using BestServices.ViewModel.Users;

namespace BestServices.ViewModel
{
    internal class AuthVM : ObservableObject
    {
        private bool IsDirtyLogin { get; set; } = default;
        private Model.DataBase.Users User { get; set; }

        public string Title => "Authorization";

        public RelayCommand GoReg { get; private set; }
        public RelayCommand Authorize { get; private set; }

        public AuthVM()
        {
            GoReg = new RelayCommand(obj => App.Messenger.Send(new SetCurrentView(new RegVM())));

            Authorize = new RelayCommand(obj => Authorization());
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                IsDirtyLogin = true;
                OnPropertyChanged();
            }
        }

        public string Password { private get; set; }

        private void Authorization()
        {
            if (IsDirtyLogin || User == null)
            {
                User = Select.SelectUser(_login);
            }

            if (User.ID != 0)
            {
                if (User.Password == Password)
                {
                    if (User.RoleID == (int)Roles.RoleType.Гость)
                    {
                        App.Messenger.Send(new SetCurrentView(new VisitorVM(User)));
                    }
                    else if (User.RoleID == (int)Roles.RoleType.Менеджер_услуг)
                    {
                        App.Messenger.Send(new SetCurrentView(new ServiceManagerVM(User)));
                    }
                }
                else
                {
                    MessageBox.Show($"Не верный пароль");
                }
            }
            else
            {
                MessageBox.Show($"Пользователь \"{_login}\" не найден");
            }

            IsDirtyLogin = false;
        }
    }
}