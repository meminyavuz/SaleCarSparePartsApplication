using AraçSatisYazilimi.NewFolder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Query
{
    public class AdminQuery
    {
        public static bool loginAdmin(string username, string password, Admin admin)
        {
            User tempUser = new User();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");
            try
            {
                connection.Open();
                if (UserQuery.GetUser(tempUser, username, password))
                {
                    admin.Id = tempUser.Id;
                    admin.Name = tempUser.Name;
                    admin.Surname = tempUser.Surname;
                    admin.Username = tempUser.Username;
                    admin.Password = tempUser.Password;
                    admin.Email = tempUser.Email;
                    admin.TelNo = tempUser.TelNo;

                    string selectQuery = "SELECT adminID FROM admin WHERE adminID = " + admin.Id;

                    SqlCommand selectCmd = new SqlCommand(selectQuery,connection);
                    
                    SqlDataReader dataReader = selectCmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }
            connection.Close();
           return false;
        }

        public static void displayAndDeleteDealer()
        {
            Dealer tempDealer = new Dealer();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();
                string selectQuery = ("SELECT dealerID FROM dealer");
                SqlCommand selectCmd = new SqlCommand(selectQuery,connection);
                List<int> dealerIds = new List<int>();
                SqlDataReader dataSelectReader = selectCmd.ExecuteReader();
                while (dataSelectReader.Read())
                {
                    int dealerID = dataSelectReader.GetInt32(0);
                    dealerIds.Add(dealerID);
                }
                dataSelectReader.Close();

                foreach(var id in dealerIds)
                {
                    
                    string displayQuery = ("SELECT * FROM users WHERE ID =" + id);

                    SqlCommand displayCmd = new SqlCommand(displayQuery, connection);
                    SqlDataReader dataDisplayReader = displayCmd.ExecuteReader();

                    while (dataDisplayReader.Read())
                    {

                        tempDealer.Id = dataDisplayReader.GetInt32(0);
                        tempDealer.Name = dataDisplayReader.GetString(1);
                        tempDealer.Surname = dataDisplayReader.GetString(2);
                        tempDealer.Username = dataDisplayReader.GetString(3);
                        tempDealer.Password = dataDisplayReader.GetString(4);
                        tempDealer.Email = dataDisplayReader.GetString(5);
                        tempDealer.TelNo = dataDisplayReader.GetString(6);

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("Id: " + tempDealer.Id);
                        Console.WriteLine("Name: " + tempDealer.Name);
                        Console.WriteLine("Surname: " + tempDealer.Surname);
                        Console.WriteLine("Username: " + tempDealer.Username);
                        Console.WriteLine("Password: " + tempDealer.Password);
                        Console.WriteLine("Email: " + tempDealer.Email);
                        Console.WriteLine("Tel No: " + tempDealer.TelNo);
                        Console.WriteLine("---------------------------");
                    }
                    dataDisplayReader.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!: "+e.Message);
            }

            Console.WriteLine("Please enter Id you want to delete!: ");
            int deleteDealerId = Convert.ToInt32(Console.ReadLine());

            string deleteDealerQuery = ("DELETE FROM dealer WHERE dealerID = "+deleteDealerId);
            SqlCommand deleteDealerCmd = new SqlCommand(deleteDealerQuery, connection);
            int rowsAffected = deleteDealerCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                string deleteUserQuery = ("DELETE FROM users WHERE ID = " + deleteDealerId);
                SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection);
                int rowsAffectedUser = deleteUserCmd.ExecuteNonQuery();

                if (rowsAffectedUser > 0)
                {
                    Console.WriteLine("Registration succsessfully deleted!");
                }
                else
                {
                    Console.WriteLine("Deletion failed. The specified ID was not found.");
                }
            }
            else
            {
                Console.WriteLine("Deletion failed. The specified ID was not found.");
            }
            connection.Close();

            
        }

        public static void displayAndDeleteCustomer()
        {
            Customer tempCustomer = new Customer();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2FKN5LJ\\MYSQLSERVER;Initial Catalog=carsale;Integrated Security=True");

            try
            {
                connection.Open();
                string selectQuery = ("SELECT customerID FROM customer");
                SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                List<int> customerIds = new List<int>();
                SqlDataReader dataSelectReader = selectCmd.ExecuteReader();
                while (dataSelectReader.Read())
                {
                    int customerID = dataSelectReader.GetInt32(0);
                    customerIds.Add(customerID);
                }
                dataSelectReader.Close();

                foreach (var id in customerIds)
                {

                    string displayQuery = ("SELECT * FROM users WHERE ID =" + id);

                    SqlCommand displayCmd = new SqlCommand(displayQuery, connection);
                    SqlDataReader dataDisplayReader = displayCmd.ExecuteReader();

                    while (dataDisplayReader.Read())
                    {

                        tempCustomer.Id = dataDisplayReader.GetInt32(0);
                        tempCustomer.Name = dataDisplayReader.GetString(1);
                        tempCustomer.Surname = dataDisplayReader.GetString(2);
                        tempCustomer.Username = dataDisplayReader.GetString(3);
                        tempCustomer.Password = dataDisplayReader.GetString(4);
                        tempCustomer.Email = dataDisplayReader.GetString(5);
                        tempCustomer.TelNo = dataDisplayReader.GetString(6);

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("Id: " + tempCustomer.Id);
                        Console.WriteLine("Name: " + tempCustomer.Name);
                        Console.WriteLine("Surname: " + tempCustomer.Surname);
                        Console.WriteLine("Username: " + tempCustomer.Username);
                        Console.WriteLine("Password: " + tempCustomer.Password);
                        Console.WriteLine("Email: " + tempCustomer.Email);
                        Console.WriteLine("Tel No: " + tempCustomer.TelNo);
                        Console.WriteLine("---------------------------");
                    }
                    dataDisplayReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!: " + e.Message);
            }

            Console.WriteLine("Please enter Id you want to delete!: ");
            int deleteCustomerId = Convert.ToInt32(Console.ReadLine());

            string deleteCustomerQuery = ("DELETE FROM customer WHERE customerID = " + deleteCustomerId);
            SqlCommand deleteCustomerCmd = new SqlCommand(deleteCustomerQuery, connection);
            int rowsAffected = deleteCustomerCmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                string deleteUserQuery = ("DELETE FROM users WHERE ID = " + deleteCustomerId);
                SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection);
                int rowsAffectedUser = deleteUserCmd.ExecuteNonQuery();

                if (rowsAffectedUser > 0)
                {
                    Console.WriteLine("Registration succsessfully deleted!");
                }
                else
                {
                    Console.WriteLine("Deletion failed. The specified ID was not found.");
                }
            }
            else
            {
                Console.WriteLine("Deletion failed. The specified ID was not found.");
            }
            connection.Close();
        }
    }
}

