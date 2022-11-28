using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestServices.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для выбора значений из Базы Данных
    /// </summary>
    internal static class Select
    {
        /// <summary>
        /// Выбирает пользователя по логину <paramref name="login"/>
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Возвращает найденного пользователя</returns>
        public static Users SelectUser(in string login)
        {
            Users user = new Users();
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "select * from [Users] where [Login] = @Login"
                    };

                    command.Parameters.Add("@Login", System.Data.SqlDbType.NVarChar, 20);
                    command.Parameters["@Login"].Value = login;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            user.ID = (int)reader["ID"];
                            user.Login = (string)reader["Login"];
                            user.Password = (string)reader["Password"];
                            user.First_Name = (string)reader["First Name"];
                            user.Last_Name = (string)reader["Last Name"];
                            user.Patronymic = (string)reader["Patronymic"];
                            user.RoleID = (int)reader["RoleID"];
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }

                    return user;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Получает значения списка услуг из БД
        /// </summary>
        /// <returns>Возвращает коллекцию усуг</returns>
        public static ICollection<Services> SelectServices()
        {
            ICollection<Services> services = new List<Services>();

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "select * from [Services]"
                    };

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            services.Add(new Services()
                            {
                                ID = (int)reader["ID"],
                                Title = (string)reader["Title"],
                                Description = (string)reader["Description"],
                                Price = (decimal)reader["Price"]
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return services;
        }

        /// <summary>
        /// Получает значения списка должностей из БД
        /// </summary>
        /// <returns>Возвращает коллекцию должностей</returns>
        public static ICollection<Roles> SelectRoles()
        {
            ICollection<Roles> roles = new List<Roles>();

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "select * from [Roles]"
                    };

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Roles()
                            {
                                ID = (int)reader["ID"],
                                RoleName= (string)reader["RoleName"]
                            });
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return roles;
        }
    }
}