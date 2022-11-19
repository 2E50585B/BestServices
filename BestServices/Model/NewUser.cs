using BestServices.Core;
using BestServices.Model.DataBase;
using System.Collections.Generic;

namespace BestServices.Model
{
    internal class NewUser : ObservableObject
    {
		public NewUser() { }

		public NewUser(Users user)
		{
			ID = user.ID;
			Login = user.Login;
			Password = user.Password;
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
				OnPropertyChanged();
			}
		}

		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				_password = value;
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

	}
}