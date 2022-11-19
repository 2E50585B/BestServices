using BestServices.Core.Messenger;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Windows;

namespace BestServices
{
    public partial class App : Application
    {
		private static IMessenger _messenger;
        internal static IMessenger Messenger
        {
            get
            {
                if (_messenger == null)
                {
                    _messenger = new Messenger();
                }

                return _messenger;
            }
        }

        internal static string ConnectionString { get; private set; }

		public App()
		{
            ConnectionString = SetConnectionString();
		}

		private static string SetConnectionString()
		{
            string connectionString = ConfigurationManager.ConnectionStrings["BestServicesEntities"].ConnectionString.ToString();
            if (connectionString.ToLower().StartsWith("metadata="))
                connectionString = new EntityConnectionStringBuilder(connectionString).ProviderConnectionString;
            return connectionString;
        }
	}
}