using System;

namespace BestServices.Core.DialogService
{
    internal class ModalDialogService : IModalDialogService
    {
        public void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose)
        {
            view.DataContext = viewModel;

            if (onDialogClose != null)
            {
                view.Closed += (s, e) => onDialogClose(viewModel);
            }

            view.Show();
        }

        public void ShowDialog<TDialogVM>(IModalWindow view, TDialogVM viewModel)
        {
            this.ShowDialog(view, viewModel, null);
        }
    }
}