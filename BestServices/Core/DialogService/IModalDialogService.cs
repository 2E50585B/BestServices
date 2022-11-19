using System;

namespace BestServices.Core.DialogService
{
    internal interface IModalDialogService
    {
        void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose);
        void ShowDialog<TDialogVM>(IModalWindow view, TDialogVM viewModel);
    }
}