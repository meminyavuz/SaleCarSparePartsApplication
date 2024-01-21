using AraçSatisYazilimi.Login;
using AraçSatisYazilimi.NewFolder;
using AraçSatisYazilimi.Vehicle;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Query
{
    public class CustomerQuery
    {
        public static void addCustomer(Customer tempCustomer)
        {
            User tempUser = new User();


            tempUser.Name = tempCustomer.Name;
            tempUser.Surname = tempCustomer.Surname;
            tempUser.Email = tempCustomer.Email;
            tempUser.Username = tempCustomer.Username;
            tempUser.Password = tempCustomer.Password;
            tempUser.TelNo = tempCustomer.TelNo;

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                UserQuery.addUser(tempUser);

                UserQuery.GetUser(tempUser, tempUser.Username, tempUser.Password);

                tempCustomer.Id = tempUser.Id;

                string insertQuery = ("INSERT INTO customer (customerID) values (@customerID)");
                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@customerID", tempCustomer.Id);
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Registration completed!");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }
            connection.Close();
        }


        public static bool loginCustomer(string username, string password, Customer customer)
        {
            User tempUser = new User();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                if (UserQuery.GetUser(tempUser, username, password))
                {
                    customer.Id = tempUser.Id;
                    customer.Name = tempUser.Name;
                    customer.Surname = tempUser.Surname;
                    customer.Username = tempUser.Username;
                    customer.Password = tempUser.Password;
                    customer.Email = tempUser.Email;
                    customer.TelNo = tempUser.TelNo;

                    string selectQuery = "SELECT customerID FROM customer WHERE customerID = " + customer.Id;

                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection);

                    SqlDataReader dataReader = selectCmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }
            connection.Close();
            return false;
        }

        public static bool deleteCustomerAccount(int customerId)
        {
            Console.Write("Are you sure you want to delete your account? Your membership will be permanently deleted[Y/y: Yes - N/n: No]: ");
            char ans = Convert.ToChar(Console.ReadLine());

            if (ans == 'Y' || ans == 'y')
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
                

                try
                {
                    connection.Open();

                    if (!isCustomerInBasket(customerId))
                    {
                        string deleteCustomerQuery = $"DELETE FROM customer WHERE customerID = {customerId}";
                        SqlCommand deleteCustomerCmd = new SqlCommand(deleteCustomerQuery, connection);
                        int rowsAffectedCustomer = deleteCustomerCmd.ExecuteNonQuery();

                        if (rowsAffectedCustomer > 0)
                        {
                            string deleteUserQuery = $"DELETE FROM users WHERE ID = {customerId}";
                            SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection);
                            int rowsAffectedUser = deleteUserCmd.ExecuteNonQuery();
                            if (rowsAffectedUser > 0)
                            {
                                Console.WriteLine("Your membership has been permanently deleted.");
                                return true;
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Your account could not be deleted because you have placed an order!");
                    }
                   connection.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error!: "+e.Message);                   
                }            
            }
            return false;
        }

        public static void addToBasket(string sparepart,int customerId,int carId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();

                string checkCarQuery = "SELECT COUNT(*) FROM car WHERE carID = @carId";
                SqlCommand checkCarCmd = new SqlCommand(checkCarQuery, connection);
                checkCarCmd.Parameters.AddWithValue("@carId", carId);

                int carCount = (int)checkCarCmd.ExecuteScalar();

                if (carCount > 0)
                {
                    string insertQuery = "INSERT INTO basket(carId, sparepart, customerId) VALUES (@carId, @sparepart, @customerId)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);

                    insertCmd.Parameters.AddWithValue("@sparepart", sparepart);
                    insertCmd.Parameters.AddWithValue("@customerId", customerId);
                    insertCmd.Parameters.AddWithValue("@carId", carId);

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Added To The Basket!");
                }
                else
                {
                    Console.WriteLine("Error: CarId not found. Item not added to the basket.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
            connection.Close();
        }

        public static bool isCustomerInBasket(int customerId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string checkCustomerQuery = "SELECT COUNT(*) FROM basket WHERE customerId = @customerId";
                SqlCommand checkCustomerCmd = new SqlCommand(checkCustomerQuery, connection);

                checkCustomerCmd.Parameters.AddWithValue("@customerId", customerId);
                
                int customerCount = (int)checkCustomerCmd.ExecuteScalar();
                if (customerCount > 0)
                {
                    return true;
                }
                else return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

    }
}
