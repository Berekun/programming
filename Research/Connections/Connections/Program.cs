﻿using System.Data.SqlClient;
using System.Text.Json;

namespace Connections
{
    internal class Program
    {
        public static void Main()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    //User user = new UserBuilder().SetName("n").Create();
                    string json = "{\"idClient\":1,\"name\":\"n\",\"age\":98,\"image\":\"A\",\"description\":\"A\",\"gender\":\"MUJER\",\"rating\":5}";
                    //string sentence1 = "EXEC updateUser '" + json + "'";
                    //string sentence2 = "EXEC addUsuario '" + json + "'";
                    string sentence3 = " SELECT * FROM dbo.GetFilteredUsers('" + json + "', 0, 10);";
                    using (SqlCommand command = new SqlCommand(sentence3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetFieldValue<int>(0);
                                string name = reader.GetFieldValue<string>(1);
                                int age = reader.GetFieldValue<int>(2);
                                string image = reader.GetFieldValue<string>(3);
                                string description = reader.GetFieldValue<string>(4);
                                string gender = reader.GetFieldValue<string>(5);
                                int rating = reader.GetFieldValue<int>(6);
                                Console.WriteLine(id);
                                Console.WriteLine(name);
                                Console.WriteLine(age);
                                Console.WriteLine(image);
                                Console.WriteLine(description);
                                Console.WriteLine(gender);
                                Console.WriteLine(rating);
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
    }
}