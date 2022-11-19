using System.Windows.Controls;

namespace BestServices.View
{
    public partial class AuthView : UserControl
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
        }
    }
}