using System.Data;
using System.Linq;
using System.Data.SqlClient;
using BestServices.Model.Security;

namespace BestServices.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для вставки значений в Базу Данных
    /// </summary>
    internal static class Insert
    {
        /// <summary>
        /// Вставляет в БД нового пользователя
        /// </summary>
        /// <param name="newUser">новый пользователь</param>
        /// <returns>Возвращает <see langword="true"/>, если удалось вставить нового пользователя
        /// или <see langword="false"/>, если <paramref name="newUser"/> уже есть в БД</returns>
        public static bool InsertUser(in NewUser newUser)
        {
            using (BestServicesEntities dataBase = new BestServicesEntities())
            {
                string login = newUser.Login;
                Users user = dataBase.Users.AsNoTracking().FirstOrDefault(u => u.Login == login);

                if (user == null)
                {
                    using (SqlConnection connection = new SqlConnection(App.ConnectionString))
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand
                            {
                                Connection = connection,
                                CommandText = @"insert into [Users] values (@Login, @Password, @FirstName, @LastName, @Patronymic, @RoleID)"
                            };

                            command.Parameters.Add("@Login", SqlDbType.NVarChar, 20);
                            command.Parameters.Add("@Password", SqlDbType.NVarChar, 20);
                            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20);
                            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 25);
                            command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 20);
                            command.Parameters.Add("@RoleID", SqlDbType.Int);

                            command.Parameters["@Login"].Value = login;
                            command.Parameters["@Password"].Value = newUser.Password.ToUnsecuredString();
                            command.Parameters["@FirstName"].Value = newUser.FirstName;
                            command.Parameters["@LastName"].Value = newUser.LastName;
                            command.Parameters["@Patronymic"].Value = newUser.Patronymic;
                            command.Parameters["@RoleID"].Value = newUser.RoleID;

                            connection.Open();

                            command.ExecuteNonQuery();

                            return true;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  Вставляет в БД новую выбранную услугу пользователя <paramref name="user"/>
        /// </summary>
        /// <param name="user">Пользователь, выбравший нову услугу</param>
        public static void InsertSelectedService(in NewUser user)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"insert into [SelectedServices] values (@UserID, @ServiceID)"
                };

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters.Add("@ServiceID", SqlDbType.Int);

                command.Parameters["@UserID"].Value = user.ID;
                command.Parameters["@ServiceID"].Value = user.SelectedServices.Last().ServiceID;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        /// <summary>
        /// Вставляет в список услуг в БД новую услугу
        /// </summary>
        /// <param name="service">Новая услуга</param>
        public static void InsertService(in Services service)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"insert into [Services] values (@Title, @Description, @Price)"
                };

                command.Parameters.Add("@Title", SqlDbType.NVarChar, 30);
                command.Parameters.Add("@Description", SqlDbType.NVarChar);
                command.Parameters.Add("@Price", SqlDbType.SmallMoney);

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