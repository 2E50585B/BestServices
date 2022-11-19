using BestServices.Core;
using BestServices.Model;
using BestServices.Model.DataBase;
using BestServices.Model.DataBase.Commands;
using System.Collections.Generic;

namespace BestServices.ViewModel.Users
{
    internal class VisitorVM : ObservableObject
    {
        public string Title => "Visitor";

        private NewUser _user;
        public NewUser User
        {
            get => _user;
            set => _user = value;
        }

        public ICollection<Services> Services { get; private set; }

        public RelayCommand SelectService { get; private set; }
        public RelayCommand DeselectService { get; private set; }

        public VisitorVM(Model.DataBase.Users user)
        {
            User = new NewUser(user);

            Services = Select.SelectServices();

            SelectService = new RelayCommand(obj =>
            {
                if (obj is SelectedServices service)
                {
                    if (!User.SelectedServices.Contains(service))
                    {
                        User.SelectedServices.Add(service);
                        Insert.InsertSelectedService(ref _user);
                    }
                }
            });

            DeselectService = new RelayCommand(obj =>
            {
                if (obj is SelectedServices service)
                {
                    if (User.SelectedServices.Contains(service))
                    {
                        User.SelectedServices.Remove(service);
                        Remove.RemoveSelectedService(ref service);
                    }
                }
            });
        }
    }
}