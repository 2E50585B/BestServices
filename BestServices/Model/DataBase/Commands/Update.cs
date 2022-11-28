using System.Data;
using System.Data.SqlClient;

namespace BestServices.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для обновления значений в Базе Данных
    /// </summary>
    internal static class Update
    {
        /// <summary>
        /// Обновляет информацию услуги <paramref name="service"/> в БД, произведя её поиск по идентификатору
        /// </summary>
        /// <param name="service">Обновлённая услуга</param>
        public static void UpdateService(in Services service)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "update [Services] set [Title] = @Title, [Description] = @Description, [Price] = @Price where [ID] = @ID"
                };

                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 30);
                command.Parameters.Add("@Description", SqlDbType.NVarChar);
                command.Parameters.Add("@Price", SqlDbType.SmallMoney);

                command.Parameters["@ID"].Value = service.ID;
                command.Parameters["@Title"].Value = service.Title;
                command.Parameters["@Description"].Value = service.Description;
                command.Parameters["@Price"].Value = service.Price;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}