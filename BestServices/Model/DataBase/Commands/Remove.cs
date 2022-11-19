using System.Data;
using System.Data.SqlClient;

namespace BestServices.Model.DataBase.Commands
{
    internal static class Remove
    {
        public static void RemoveSelectedService(ref SelectedServices service)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "delete from [SelectedServices] where [ID] = @SelectedServicesID"
                };

                command.Parameters.Add("@SelectedServicesID", SqlDbType.Int);

                command.Parameters["@SelectedServicesID"].Value = service.ID;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void RemoveService(ref Services service)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "delete from [Services] where [ID] = @ServiceID"
                };

                command.Parameters.Add("@ServiceID", SqlDbType.Int);

                command.Parameters["@ServiceID"].Value = service.ID;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}