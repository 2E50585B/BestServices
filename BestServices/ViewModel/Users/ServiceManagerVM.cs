using BestServices.Core;
using BestServices.Model;
using BestServices.Model.DataBase;
using BestServices.Model.DataBase.Commands;
using System.Collections.Generic;
using System.Linq;

namespace BestServices.ViewModel.Users
{
    internal class ServiceManagerVM : ObservableObject
    {
        private List<Services> ServicesCollection { get; set; }

        public string Title => "Service Manager";

        public NewUser User { get; private set; }

        public List<Services> FilteredServices =>
            new List<Services>((from service in ServicesCollection
                                where service.Title.ToLower().Contains(_filter?.ToLower() ?? "")
                                select service).ToList());

        public RelayCommand AddService { get; private set; }
        public RelayCommand DeleteService { get; private set; }
        public RelayCommand EditService { get; private set; }

        public ServiceManagerVM(Model.DataBase.Users user)
        {
            User = new NewUser(user);

            ServicesCollection = (List<Services>)Select.SelectServices();

            AddService = new RelayCommand(obj =>
            {
                //Instance DialogBox
                Services service = new Services();//service from DialogBoxVM
                ServicesCollection.Add(service);
                OnPropertyChanged("FilteredServices");
                Insert.InsertService(ref service);
            });

            DeleteService = new RelayCommand(obj =>
            {
                if (obj is Services service)
                {
                    ServicesCollection.Remove(service);
                    OnPropertyChanged("FilteredServices");
                    Remove.RemoveService(ref service);
                }
            });

            EditService = new RelayCommand(obj =>
            {
                if (obj is Services service)
                {
                    //Instance DialogBox
                    Services newService = new Services();//service from DialogBoxVM with old serviceID
                    ServicesCollection[ServicesCollection.IndexOf(service)] = newService;
                    ServicesCollection.Remove(service);
                    OnPropertyChanged("FilteredServices");
                    Update.UpdateService(ref newService);
                }
            });
        }

        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged("FilteredServices");
            }
        }

    }
}