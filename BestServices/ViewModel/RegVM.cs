using BestServices.Core;
using BestServices.Model;
using BestServices.Model.DataBase;
using BestServices.Model.DataBase.Commands;
using BestServices.Model.Messages;
using System.Collections.Generic;
using System.Windows;

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

        public IList<Roles> Roles { get; private set; }

        public RelayCommand ReturnCommand { get; private set; }
        public RelayCommand SignUpCommand { get; private set; }

        public RegVM()
        {
            NewUser = new NewUser();

            Roles = new List<Roles>(Select.SelectRoles());

            ReturnCommand = new RelayCommand(obj => App.Messenger.Send(new SetCurrentView(new AuthVM())));

            SignUpCommand = new RelayCommand(obj =>
            {
                if (!NewUser.IsFieldsEmpty())
                {
                    if (Insert.InsertUser(ref _newUser))
                    {
                        MessageBox.Show("Registration Completed Successfully!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Registration Failed With En ERROR!\nThis Login Is Busy:\t{NewUser.Login}", "FAILURE",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Not All Fields Are Filled In!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private Roles _selectedRole;
        public Roles SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                NewUser.Role = value.Role;
                OnPropertyChanged();
            }
        }

    }
}