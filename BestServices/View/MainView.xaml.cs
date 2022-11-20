using System.Windows;

namespace BestServices.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}