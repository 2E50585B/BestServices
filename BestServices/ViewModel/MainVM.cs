using BestServices.Core;
using BestServices.Model.Messages;

namespace BestServices.ViewModel
{
    internal class MainVM : ObservableObject
    {
        public string Title => "Main Page";

		private object _currentView;
		public object CurrentView
		{
			get => _currentView;
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand GoHome { get; private set; }
		public HomeVM HomeVM { get; private set; }

		public MainVM()
		{
			App.Messenger.Subscribe<SetCurrentView>(this, obj =>
			{
				if (obj is SetCurrentView message)
				{
					CurrentView = message.ViewModel;
				}
			});

			HomeVM = new HomeVM();

			CurrentView = HomeVM;

			GoHome = new RelayCommand(obj => CurrentView = HomeVM);
		}
	}
}