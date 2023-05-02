using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Mikender
{
    internal class Connection
    {
        public static void Connect()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    User user = new UserBuilder().SetName("N").Create();
                    string json = JsonSerializer.Serialize<User>(user);
                    //string sentence1 = "EXEC updateUser @value";
                    //string sentence2 = "EXEC addUsuario @value";
                    //string sentence3 = "EXEC RemoveUser @value";
                    string sentence4 = " SELECT * FROM dbo.GetFilteredUsers(@value, 0, 10);";
                    using (SqlCommand command = new SqlCommand(sentence4, connection))
                    {
                        command.Parameters.AddWithValue("@value",json);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //int id = reader.GetFieldValue<int>(0);
                                string name = reader.GetFieldValue<string>(1);
                                int age = reader.GetFieldValue<int>(2);
                                //string image = reader.GetFieldValue<string>(3);
                                string description = reader.GetFieldValue<string>(4);
                                //string gender = reader.GetFieldValue<string>(5);
                                //int rating = reader.GetFieldValue<int>(6);
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

        public static List<User> SearchUser(string name, int offset, int limit)
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    User user = new UserBuilder().SetName(name).Create();
                    string json = JsonSerializer.Serialize<User>(user);
                    string sentence4 = " SELECT * FROM dbo.GetFilteredUsers(@value, @offset, @limit);";
                    using (SqlCommand command = new SqlCommand(sentence4, connection))
                    {
                        command.Parameters.AddWithValue("@value", json);
                        command.Parameters.AddWithValue("@offset", offset);
                        command.Parameters.AddWithValue("@limit", limit);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User()
                                {
                                    idClient = reader.GetFieldValue<int>(0),
                                    name = reader.GetFieldValue<string>(1),
                                    age = reader.GetFieldValue<int>(2),
                                    image = reader.GetFieldValue<string>(3),
                                    description = reader.GetFieldValue<string>(4),
                                    gender = reader.GetFieldValue<string>(5),
                                    rating = reader.GetFieldValue<int>(6)
                                });       
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }

            return users;
        }
    }
}
