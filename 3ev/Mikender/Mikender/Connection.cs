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
    public class Connection
    {
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

        public static void AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    string json = JsonSerializer.Serialize<User>(user);
                    string sentence2 = "EXEC addUsuario @value";
                    using (SqlCommand command = new SqlCommand(sentence2, connection))
                    {
                        command.Parameters.AddWithValue("@value", json);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }

        public static void RemoveUser(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    string sentence2 = "EXEC RemoveUser @value";
                    using (SqlCommand command = new SqlCommand(sentence2, connection))
                    {
                        command.Parameters.AddWithValue("@value", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=MIKENDER;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    string json = JsonSerializer.Serialize<User>(user);
                    string sentence2 = "EXEC updateUser @value";
                    using (SqlCommand command = new SqlCommand(sentence2, connection))
                    {
                        command.Parameters.AddWithValue("@value", json);

                        command.ExecuteNonQuery();
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
