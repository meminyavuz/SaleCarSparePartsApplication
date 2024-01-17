using AraçSatisYazilimi.NewFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Query
{
    public class DealerQuery
    {
        public static void addDealer(Dealer tempDealer)
        {
            User tempUser = new User();


            tempUser.Name = tempDealer.Name;
            tempUser.Surname = tempDealer.Surname;
            tempUser.Email = tempDealer.Email;
            tempUser.Username = tempDealer.Username;
            tempUser.Password = tempDealer.Password;
            tempUser.TelNo = tempDealer.TelNo;

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                UserQuery.addUser(tempUser);

                UserQuery.GetUser(tempUser, tempUser.Username, tempUser.Password);

                tempDealer.Id = tempUser.Id;

                string insertQuery = ("INSERT INTO dealer (dealerID) values (@dealerID)");
                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@dealerID", tempDealer.Id);
                insertCmd.ExecuteNonQuery();
                 Console.WriteLine("Registration completed!");
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
            connection.Close();
        }



        public static bool loginDealer(string username, string password, Dealer dealer)
        {
            User tempUser = new User();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                if (UserQuery.GetUser(tempUser, username, password))
                {
                    dealer.Id = tempUser.Id;
                    dealer.Name = tempUser.Name;
                    dealer.Surname = tempUser.Surname;
                    dealer.Username = tempUser.Username;
                    dealer.Password = tempUser.Password;
                    dealer.Email = tempUser.Email;
                    dealer.TelNo = tempUser.TelNo;

                    string selectQuery = "SELECT dealerID FROM dealer WHERE dealerID = " + dealer.Id;

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

        public static void displaySalesRequest()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                string selectQuery = "SELECT * FROM basket";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                SqlDataReader basketReader = selectCmd.ExecuteReader();

                while (basketReader.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("Basket ID: "+(int)basketReader.GetValue(0));
                    Console.WriteLine("Car ID:"+(int)basketReader.GetValue(1));
                    Console.WriteLine("Car Brand: "+basketReader.GetValue(2).ToString());
                    Console.WriteLine("Car Model: "+basketReader.GetValue(3).ToString());
                    Console.WriteLine("Car Packet: "+basketReader.GetValue(4).ToString());
                    Console.WriteLine("Spare Part: "+basketReader.GetValue(5).ToString());
                    Console.WriteLine("Customer ID: "+basketReader.GetValue(6));
                    Console.WriteLine("Customer Name: "+basketReader.GetValue(7).ToString());
                    Console.WriteLine("Customer Surname: "+basketReader.GetValue(8).ToString());
                    Console.WriteLine("Customer Email: "+basketReader.GetValue(9).ToString());
                    Console.WriteLine("Customer Phone: "+basketReader.GetValue(10).ToString());
                    Console.WriteLine("--------------------------------------------");

                }
                basketReader.Close();
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
        }

        public static void moveToLog(int basketId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO log (basketID, carID, car_brand, car_model, car_packet, spare_part, customerID, customer_name, customer_surname, customer_email, customer_phone) SELECT basketID, carID, car_brand, car_model, car_packet, spare_part, customerID, customer_name, customer_surname, customer_email, customer_phone FROM basket WHERE basketID = @basketId;";

                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@basketID", basketId);

                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Sales transaction successful!");

                string selectQuery = "SELECT carID,spare_part FROM basket WHERE basketID = @basketId";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                selectCmd.Parameters.AddWithValue("@basketID", basketId);
                SqlDataReader readSparePart = selectCmd.ExecuteReader();

                if (readSparePart.Read())
                {
                    int temp_car_id = (int)readSparePart.GetValue(0);
                    string temp_spare_part = readSparePart.GetValue(1).ToString();
                    updateCarStock(temp_spare_part,temp_car_id);
                    deleteBasket(basketId);
                }
                readSparePart.Close();

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void deleteBasket(int basketId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();
                string deleteQuery = "DELETE FROM basket WHERE basketID = @BasketId";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);

                deleteCmd.Parameters.AddWithValue("@BasketId", basketId);
                deleteCmd.ExecuteNonQuery();

                Console.WriteLine("Deletion Successful!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void updateCarStock(string sparePart, int carId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string updateQuery = $"UPDATE car SET {sparePart} = {sparePart} - 1 WHERE carID = {carId} ";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Stocks updated!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
