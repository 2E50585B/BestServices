using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BestServices.Model.DataBase.Commands
{
    internal static class Select
    {
        public static Users SelectUser(string login)
        {
            using (BestServicesEntities dataBase = new BestServicesEntities())
            {
                return dataBase.Users.AsNoTracking().FirstOrDefault(u => u.Login == login);
            }
        }

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
    }
}