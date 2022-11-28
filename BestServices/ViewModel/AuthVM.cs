using System;
using System.Linq;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BestServices.Core;
using BestServices.Model.DataBase;
using BestServices.Model.Messages;
using BestServices.Model.Security;
using BestServices.Model.DataBase.Commands;
using BestServices.ViewModel.Users;

namespace BestServices.ViewModel
{
    internal class AuthVM : ObservableObject, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        private bool IsDirtyLogin { get; set; } = default;
        private Model.DataBase.Users User { get; set; }

        public string Title => "Authorization";

        public RelayCommand GoReg { get; private set; }
        public RelayCommand Authorize { get; private set; }

        public AuthVM()
        {
            GoReg = new RelayCommand(obj => App.Messenger.Send(new SetCurrentView(new RegVM())));

            Authorize = new RelayCommand(obj => Authorization(), obj => !HasErrors);
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                IsDirtyLogin = true;
                ClearErrors();
                if (value.Length < 5)
                {
                    AddError("Логин не может быть меньше 5 символов!");
                }
                OnPropertyChanged();
            }
        }

        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                ClearErrors();
                OnPropertyChanged();
            }
        }

        public bool HasErrors => _propertyErrors.Any();

        private void Authorization()
        {
            if (IsDirtyLogin || User == null)
            {
                User = Select.SelectUser(_login);
            }

            if (User.ID != 0)
            {
                if (User.Password == Password.ToUnsecuredString())
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
                    AddError("Не верный пароль", nameof(Password));
                }
            }
            else
            {
                AddError($"Пользователь \"{_login}\" не найден", nameof(Login));
            }

            IsDirtyLogin = false;
        }

        public IEnumerable GetErrors(string propertyName) =>
            _propertyErrors.ContainsKey(propertyName) ? _propertyErrors[propertyName] : null;

        public void AddError(string errorMessage, [CallerMemberName] string propertyName = "")
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors([CallerMemberName] string propertyName = "")
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}