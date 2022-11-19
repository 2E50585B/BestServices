using BestServices.Core;
using BestServices.Model.Messages;
using System.Windows;

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
		public RelayCommand GoAuth { get; private set; }
		public RelayCommand GoReg { get; private set; }

		public RelayCommand DefaultSize { get; private set; }
		public RelayCommand Maximize { get; private set; }
		public RelayCommand Minimize { get; private set; }

		public RelayCommand Exit { get; private set; }

		public HomeVM HomeVM { get; private set; }
		public AuthVM AuthVM { get; private set; }
		public RegVM RegVM { get; private set; }

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
			AuthVM = new AuthVM();
			RegVM= new RegVM();

			CurrentView = HomeVM;

			GoHome = new RelayCommand(obj => CurrentView = HomeVM, obj => CurrentView != HomeVM);
			GoAuth = new RelayCommand(obj => CurrentView = AuthVM, obj => CurrentView != AuthVM);
			GoReg = new RelayCommand(obj => CurrentView = RegVM, obj => CurrentView != RegVM);

			DefaultSize = new RelayCommand(obj => Application.Current.MainWindow.WindowState = WindowState.Normal,
				obj => Application.Current.MainWindow.WindowState != WindowState.Normal);
			Maximize = new RelayCommand(obj => Application.Current.MainWindow.WindowState = WindowState.Maximized,
				obj => Application.Current.MainWindow.WindowState != WindowState.Maximized);
			Minimize = new RelayCommand(obj => Application.Current.MainWindow.WindowState = WindowState.Minimized,
				obj => Application.Current.MainWindow.WindowState != WindowState.Minimized);

			Exit = new RelayCommand(obj => Application.Current.Shutdown());
		}
	}
}