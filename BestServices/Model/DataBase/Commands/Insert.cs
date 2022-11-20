using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace BestServices.Model.DataBase.Commands
{
    internal static class Insert
    {
        public static bool InsertUser(ref NewUser newUser)
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
                            command.Parameters["@Password"].Value = newUser.Password;
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

        public static void InsertSelectedService(ref NewUser newUser)
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

                command.Parameters["@UserID"].Value = newUser.ID;
                command.Parameters["@ServiceID"].Value = newUser.SelectedServices.Last().ServiceID;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void InsertService(ref Services service)
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