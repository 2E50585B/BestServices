using System;

namespace BestServices.Core.DialogService
{
    /// <summary>
    /// Определяет диалоговое окно
    /// </summary>
    internal interface IModalWindow
    {
        bool? Result { get; set; }
        event EventHandler OnClose;
        void ShowView();
        object Data { get; set; }
        void CloseView();
    }
}