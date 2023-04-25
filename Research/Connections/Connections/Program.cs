using System.Data.SqlClient;

namespace Connections
{
    internal class Program
    {
        public static void Main()
        {
            try 
            {
                using (SqlConnection connection = new SqlConnection("Data Source=192.168.56.101,1433;Initial Catalog=USERS;User ID=sa;Password=SqlServer123;Connection Timeout=30"))
                {
                    connection.Open();
                    string sentence1 = "SELECT name, age FROM USUARIOS WHERE name LIKE @value";
                    string sentence2 = "INSERT INTO USUARIOS(name,age) VALUES(@value1,@value2)";
                    string sentence3 = "EXEC addUser @value1, @value2";
                    string sentence4 = "SELECT dbo.getUserName(@value)";

                    using (SqlCommand command = new SqlCommand(sentence4, connection))
                    {
                        command.Parameters.AddWithValue("@value", 2);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetFieldValue<string>(0);
                                Console.WriteLine(name);
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