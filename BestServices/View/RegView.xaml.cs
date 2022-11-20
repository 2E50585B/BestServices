using System.Windows.Controls;

namespace BestServices.View
{
    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((dynamic)DataContext).NewUser.Password = ((PasswordBox)sender).Password;
        }
    }
}