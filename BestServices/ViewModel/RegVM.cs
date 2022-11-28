using System.Collections.Generic;
using BestServices.Core;
using BestServices.Core.DialogService;
using BestServices.Model;
using BestServices.Model.DataBase;
using BestServices.Model.DataBase.Commands;
using BestServices.Model.Messages;
using BestServices.View.Dialogs;
using BestServices.ViewModel.Dialogs;

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
                    if (Insert.InsertUser(_newUser))
                    {
                        App.ModalDialogService.ShowDialog(new SimpleNotification(),
                            new SimpleNotificationVM("УСПЕХ", "Регистрация завершена успешно!"), DialogType.Notify);
                    }
                    else
                    {
                        App.ModalDialogService.ShowDialog(new SimpleNotification(),
                            new SimpleNotificationVM("НЕУДАЧА",
                            $"Регистрация завершена с ошибкой!\nЭтот логин уже используется:\t{NewUser.Login}"), DialogType.Error);
                    }
                }
                else
                {
                    App.ModalDialogService.ShowDialog(new SimpleNotification(),
                        new SimpleNotificationVM("ОШИБКА", "Не все поля заполнены!"), DialogType.Error);
                }
            }, obj => !NewUser.HasErrors);
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