using BestServices.Core;
using BestServices.Model.DataBase;
using BestServices.Model.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;

namespace BestServices.Model
{
    /// <summary>
    /// Представляет собой объект пользователя, схожий с <see cref="Users"/>, но имеющий валидацию данных и оповещающий об изменении своих свойств
    /// </summary>
    internal class NewUser : ObservableObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public NewUser() { }

        /// <summary>
        /// Создаёт экземпляр объекта пользователя с данными из БД (от <paramref name="user"/>)
        /// </summary>
        /// <param name="user">Пользователь из БД</param>
        public NewUser(Users user)
        {
            ID = user.ID;
            Login = user.Login;
            Password.ToSecuredString(user.Password);
            FirstName = user.First_Name;
            LastName = user.Last_Name;
            Patronymic = user.Patronymic;
            Role = (Roles.RoleType)user.RoleID;
            SelectedServices = new List<SelectedServices>(user.SelectedServices);
        }

        public int ID { get; private set; }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                ClearErrors();
                if (value.Length < 5)
                {
                    AddError("Логин не может быть меньше 5 символов!");
                }
                if (value.Contains(" "))
                {
                    AddError("Пароль не может содержать символы пробела!");
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
                if (value.Length < 5)
                {
                    AddError("Пароль не может быть меньше 5 символов!");
                }
                if (value.ToUnsecuredString().Contains(" "))
                {
                    AddError("Пароль не может содержать символы пробела!");
                }
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                ClearErrors();
                if (value.Length < 2)
                {
                    AddError("Имя не может быть меньше 2 символов!");
                }
                if (value.Trim().All(char.IsLetter) == false)
                {
                    AddError("Имя может содержать только буквы!");
                }
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                ClearErrors();
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError("Фамилия не может быть пустой!");
                }
                if (value.Trim().All(char.IsLetter) == false)
                {
                    AddError("Фамилия может содержать только буквы!");
                }
                OnPropertyChanged();
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                ClearErrors();
                if (value.Trim().All(char.IsLetter) == false)
                {
                    AddError("Отчество может содержать только буквы!");
                }
                OnPropertyChanged();
            }
        }

        public int RoleID { get; private set; }

        private Roles.RoleType _role;
        public Roles.RoleType Role
        {
            get => _role;
            set
            {
                _role = value;
                RoleID = (int)value;
                OnPropertyChanged();
            }
        }

        private ICollection<SelectedServices> _selectedServices;
        public ICollection<SelectedServices> SelectedServices
        {
            get => _selectedServices;
            set
            {
                _selectedServices = value;
                OnPropertyChanged();
            }
        }

        public bool IsFieldsEmpty() => string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(_password.ToUnsecuredString()) ||
            string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || RoleID == default;

        public bool HasErrors => _propertyErrors.Any();

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