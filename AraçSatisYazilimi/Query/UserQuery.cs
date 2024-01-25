using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Query
{
    internal class UserQuery
    {
        public static void addUser(User tempUser)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO users (name, surname, username, password, email, phone) VALUES (@Name, @Surname, @Username, @Password, @Email, @TelNo)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);

                insertCmd.Parameters.AddWithValue("@Name", tempUser.Name);
                insertCmd.Parameters.AddWithValue("@Surname", tempUser.Surname);
                insertCmd.Parameters.AddWithValue("@Username", tempUser.Username);
                insertCmd.Parameters.AddWithValue("@Password", tempUser.Password);
                insertCmd.Parameters.AddWithValue("@Email", tempUser.Email);
                insertCmd.Parameters.AddWithValue("@TelNo", tempUser.TelNo);

                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Addition Completed Successfully!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }
            connection.Close();
        }

        public static bool GetUser(User tempUser, string username, string password)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();

                string selectQuery = "SELECT * FROM users";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                SqlDataReader dataReader = selectCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    tempUser.Id = (int)dataReader.GetValue(0);
                    tempUser.Name = dataReader.GetValue(1).ToString();
                    tempUser.Surname = dataReader.GetValue(2).ToString();
                    tempUser.Username = dataReader.GetValue(3).ToString();
                    tempUser.Password = dataReader.GetValue(4).ToString();
                    tempUser.Email = dataReader.GetValue(5).ToString();
                    tempUser.TelNo = dataReader.GetValue(6).ToString();

                    if (dataReader.GetValue(3).ToString() == username && dataReader.GetValue(4).ToString() == password) 
                    {
                        connection.Close();
                        return true;
                    }                                                      
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
                
            }
            connection.Close();

            return false;
        }

        //kullanıcı adının kullanıp kullanılmadığını anlamak için fonksiyon
        public static bool isUsernameNotUsed(string username)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                //fonksiyonda parametre olarak gelen username verisini users tablosunda karşılaştır aynı değer bulunursa sayaç arttır.
                string selectQuery = "SELECT COUNT(*) FROM users WHERE username = @username";

                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                selectCmd.Parameters.AddWithValue("@Username", username);

                int count = (int)selectCmd.ExecuteScalar();

                if (count > 0)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }
            connection.Close();
            return false;
        }
    }
}

   
