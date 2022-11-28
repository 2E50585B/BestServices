using System;
using System.Windows;
using BestServices.Core.DialogService;

namespace BestServices.View.Dialogs
{
    public partial class SimpleNotification : IModalWindow
    {
        public SimpleNotification()
        {
            InitializeComponent();
        }

        public bool? Result { get; set; }
        public object Data { get; set; }

        public event EventHandler OnClose;

        public void CloseView()
        {
            Close();
            OnClose?.Invoke(this, EventArgs.Empty);
        }

        public void ShowView()
        {
            DataContext = Data;
            ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            DialogResult = Result;
        }
    }
}