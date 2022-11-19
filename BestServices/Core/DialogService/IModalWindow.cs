using System;

namespace BestServices.Core.DialogService
{
    internal interface IModalWindow
    {
        bool? DialogResult { get; set; }
        event EventHandler Closed;
        void Show();
        object DataContext { get; set; }
        void Close();
    }
}