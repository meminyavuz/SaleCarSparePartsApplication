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

        public static bool deleteAccount(int customerId)
        {
            Console.Write("Are you sure you want to delete your account? Your membership will be permanently deleted[Y/y: Yes - N/n: No]: ");
            char ans = Convert.ToChar(Console.ReadLine());

            if (ans == 'Y' || ans == 'y')
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
                

                try
                {
                    connection.Open();
                    string deleteCustomerQuery = $"DELETE FROM customer WHERE customerID = {customerId}";
                    SqlCommand deleteCustomerCmd = new SqlCommand(deleteCustomerQuery,connection);
                    int rowsAffectedCustomer = deleteCustomerCmd.ExecuteNonQuery();

                    if(rowsAffectedCustomer > 0)
                    {
                        string deleteUserQuery = $"DELETE FROM users WHERE ID = {customerId}";
                        SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection);
                        int rowsAffectedUser = deleteUserCmd.ExecuteNonQuery();
                        if( rowsAffectedUser > 0)
                        {
                            Console.WriteLine("Your membership has been permanently deleted.");
                            connection.Close();
                            return true;
                        }                       
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error!: "+e.Message);                   
                }            
            }
            return false;
        }

        public static void addToBasket(string spare_part,Customer tempCustomer,Car tempCar)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();

                string selectQuery = $"SELECT brand_name,car_model,car_packet FROM car WHERE carID = {tempCar.Id}";
                SqlCommand selectCmd = new SqlCommand(selectQuery,connection);
                SqlDataReader readerCar = selectCmd.ExecuteReader();
                while (readerCar.Read())
                {
                    tempCar.BrandName = readerCar.GetValue(0).ToString();
                    tempCar.Model = readerCar.GetValue(1).ToString();
                    tempCar.Packet = readerCar.GetValue(2).ToString();
                }
                readerCar.Close();

                string selectCustomerQuery = $"SELECT name,surname,email,phone FROM users WHERE ID = {tempCustomer.Id}";
                SqlCommand selectCustomerCmd = new SqlCommand(selectCustomerQuery, connection);
                SqlDataReader readerCustomer = selectCustomerCmd.ExecuteReader();

                while (readerCustomer.Read())
                {
                    tempCustomer.Name = readerCustomer.GetValue(0).ToString();
                    tempCustomer.Surname = readerCustomer.GetValue(1).ToString();
                    tempCustomer.Email = readerCustomer.GetValue(2).ToString();
                    tempCustomer.TelNo = readerCustomer.GetValue(3).ToString();
                }
                readerCustomer.Close();

                string insertQuery = "INSERT INTO basket (carID,car_brand,car_model,car_packet,spare_part,customerID,customer_name,customer_surname,customer_email,customer_phone) values (@carID,@car_brand,@car_model,@car_packet,@spare_part,@customerID,@customer_name,@customer_surname,@customer_email,@customer_phone) ";

                SqlCommand insertCmd = new SqlCommand(insertQuery,connection);

                insertCmd.Parameters.AddWithValue("@carID",tempCar.Id);
                insertCmd.Parameters.AddWithValue("@car_brand",tempCar.BrandName);
                insertCmd.Parameters.AddWithValue("@car_model",tempCar.Model);
                insertCmd.Parameters.AddWithValue("@car_packet",tempCar.Packet);
                insertCmd.Parameters.AddWithValue("@spare_part",spare_part);
                insertCmd.Parameters.AddWithValue("@customerID",tempCustomer.Id);
                insertCmd.Parameters.AddWithValue("@customer_name",tempCustomer.Name);
                insertCmd.Parameters.AddWithValue("@customer_surname",tempCustomer.Surname);
                insertCmd.Parameters.AddWithValue("@customer_email",tempCustomer.Email);
                insertCmd.Parameters.AddWithValue("@customer_phone",tempCustomer.TelNo);

                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Added To The Basket!");

            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
        }

    }
}
