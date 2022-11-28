using BestServices.Core;

namespace BestServices.Model.Messages
{
    /// <summary>
    /// Передаёт новый объект ViewModel для смены текущей View
    /// </summary>
    internal class SetCurrentView
    {
        public ObservableObject ViewModel { get; private set; }

        /// <summary>
        /// Уведомляет о смене текущей View и сохраняет объект ViewModel
        /// </summary>
        public SetCurrentView(ObservableObject viewModel)
        {
            ViewModel = viewModel;
            App.Messenger.Send(this);
        }
    }
}