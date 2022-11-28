using System.Data;
using System.Data.SqlClient;

namespace BestServices.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для удаления значений из Базы Данных
    /// </summary>
    internal static class Remove
    {
        /// <summary>
        /// Удаляет выбранную услугу из соответствующего списка в БД
        /// </summary>
        /// <param name="service">Выбранная услуга</param>
        public static void RemoveSelectedService(in SelectedServices service)
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

        /// <summary>
        /// Удаляет услугу из списка услуг в БД
        /// </summary>
        /// <param name="service">Услуга</param>
        public static void RemoveService(in Services service)
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