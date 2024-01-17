using AraçSatisYazilimi.Vehicle;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Query
{
    public class CarQuery
    {
        public static void addCar(Car tempCar)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO car (brand_name, car_model, car_packet) VALUES (@Brand_name, @Car_model, @Car_packet)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, connection);

                insertCmd.Parameters.AddWithValue("@Brand_name", tempCar.BrandName);
                insertCmd.Parameters.AddWithValue("@Car_model", tempCar.Model);
                insertCmd.Parameters.AddWithValue("@Car_packet", tempCar.Packet);
            
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Addition Completed Successfully!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }
            connection.Close();
        }

        public static bool IsUsedInBasket(int carId)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM basket WHERE carID = @CarId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);

                checkCmd.Parameters.AddWithValue("@CarId", carId);

                int count = (int)checkCmd.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }  
        }

        public static void deleteCar(int carId)
        {
            
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                if (IsUsedInBasket(carId))
                {
                    Console.WriteLine("Deletion failed! Car is used in the basket.");
                }
                else
                {
                    string deleteQuery = "DELETE FROM car WHERE carID = (@CarId)";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);

                    deleteCmd.Parameters.AddWithValue("@CarId", carId);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Deletion Completed Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Deletion failed! Car with the specified ID not found.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Deletion failed!, "+e.Message);
            }
        }

        public static void displayAndUpdateStocks()
        {
            
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string selectQuery = "SELECT * FROM car";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);

                SqlDataReader dataSelectReader = selectCmd.ExecuteReader();
                while (dataSelectReader.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Car's Id: " + dataSelectReader.GetInt32(0));
                    Console.WriteLine("Car's Brand: " + dataSelectReader.GetString(1));
                    Console.WriteLine("Car's Model: " + dataSelectReader.GetString(2));
                    Console.WriteLine("Car's Packet: " + dataSelectReader.GetString(3));
                    Console.WriteLine("Engine Stock: " + dataSelectReader.GetInt32(4));
                    Console.WriteLine("Clutch Stock: " + dataSelectReader.GetInt32(5));
                    Console.WriteLine("Exhaust Stock: " + dataSelectReader.GetInt32(6));
                    Console.WriteLine("Brakes Stock: " + dataSelectReader.GetInt32(7));
                    Console.WriteLine("Battery Stock: " + dataSelectReader.GetInt32(8));
                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------");

                }
                dataSelectReader.Close();

                Console.Write("Please enter the ID of the car whose stock you want to update!: ");
                int carChoose = Convert.ToInt32(Console.ReadLine());
                Console.Write("Please write the product whose stock number you want to update!: ");
                string stockChoose = Console.ReadLine();
                Console.Write("Please enter the stock quantity you want to update!: ");
                int stockQuantity = Convert.ToInt32(Console.ReadLine());

                string updateQuery = $"UPDATE car SET {stockChoose} = {stockQuantity} WHERE carID = {carChoose}";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);

                int affectedRows = updateCmd.ExecuteNonQuery();

                if (affectedRows > 0) Console.WriteLine("Your Stocks Updated!");
                else Console.WriteLine("Update Failure!");               
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
            connection.Close();
        }

        public static void listCar()
        {
            Car tempCar = new Car();

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();

                string selectQuery = "SELECT * FROM car";
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);

                SqlDataReader dataSelectReader = selectCmd.ExecuteReader();
                while (dataSelectReader.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Car's Id: " + dataSelectReader.GetInt32(0));
                    Console.WriteLine("Car's Brand: " + dataSelectReader.GetString(1));
                    Console.WriteLine("Car's Model: " + dataSelectReader.GetString(2));
                    Console.WriteLine("Car's Packet: " + dataSelectReader.GetString(3));
                    Console.WriteLine("");
                    Console.WriteLine("----------------------------------------");

                }
                dataSelectReader.Close();
            }
            catch
            {
                Console.WriteLine("Error!");
            }
            connection.Close();
        }
    }    
}
