using AraçSatisYazilimi.NewFolder;
using AraçSatisYazilimi.Query;
using AraçSatisYazilimi.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AraçSatisYazilimi.Login
{
    internal class Login
    {
        public static void mainPage()
        {
            int flag = 0;
            while(flag == 0)
            {
                Console.WriteLine("------Welcome Auto Spare Parts Sale App------");
                Console.WriteLine("1-Admin Page");
                Console.WriteLine("2-Dealer Page");
                Console.WriteLine("3-Customer Page");
                Console.WriteLine("4-Exit");

                int ans = Convert.ToInt32(Console.ReadLine());

                switch (ans)
                {
                    case 1:
                        adminMainPage();
                        break;

                    case 2:
                        dealerMainPage();
                        break;

                    case 3:
                        customerMainPage();
                        break;

                    case 4:
                        flag = 1;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
        }

        public static void adminMainPage()
        {
            Admin admin = new Admin();

            Console.Write("Enter the username: ");
            String username = Console.ReadLine();

            Console.Write("Enter the password: ");
            String password = Console.ReadLine();

            if(AdminQuery.loginAdmin(username,password,admin)) adminMenuPage(admin);
            else Console.WriteLine("Your username or password is wrong!");
        }

        public static void adminMenuPage(Admin admin)
        {
            int flag = 1;

            while (flag == 1)
            {
                Console.WriteLine("");
                Console.WriteLine("------ADMIN MENU PAGE------");
                Console.WriteLine("");
                Console.WriteLine($"Welcome {admin.Name} {admin.Surname}");
                Console.WriteLine("");
                Console.WriteLine("1-Delete Dealer");
                Console.WriteLine("2-Delete Customer");
                Console.WriteLine("3-Delete Cars");
                Console.WriteLine("4-Account Information");
                Console.WriteLine("5-Exit");
                Console.Write("Please enter your selection: ");
                int ans = Convert.ToInt32(Console.ReadLine());
                

                switch (ans)
                {
                    case 1:

                        AdminQuery.displayAndDeleteDealer();
                        break;

                    case 2:
                        AdminQuery.displayAndDeleteCustomer();
                        break;

                    case 3:
                        CarQuery.listCar();
                        Console.WriteLine("Please enter the ID of the car you want to delete!:");
                        int ansDelete = Convert.ToInt32(Console.ReadLine());
                        CarQuery.deleteCar(ansDelete);
                        break;

                    case 4:
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(admin.ToString());
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("");
                        break;

                    case 5:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
        }

        public static void dealerMainPage()
        {
            Dealer dealer = new Dealer();
            int flag = 1;
            while(flag == 1)
            {
                Console.WriteLine("");
                Console.WriteLine("------DEALER MAIN PAGE------");
                Console.WriteLine("");
                Console.WriteLine("1-Sign in");
                Console.WriteLine("2-Sign up");
                Console.WriteLine("3-Exit");
                Console.Write("Please enter your selection: ");

                int ans = Convert.ToInt32(Console.ReadLine());

                switch (ans)
                {
                    case 1:
                        Console.Write("Enter the username: ");
                        string username = Console.ReadLine();

                        Console.Write("Enter the password: ");
                        string password = Console.ReadLine();

                        if(DealerQuery.loginDealer(username, password, dealer)) dealerMenuPage(dealer);
                        else Console.WriteLine("Your username or password is wrong!");

                        break;

                    case 2:          
                        Console.Write("Enter name: ");
                        dealer.Name = Convert.ToString(Console.ReadLine());

                        if(dealer.Name != null)
                        {
                            Console.Write("Enter Surname: ");
                            dealer.Surname = Convert.ToString(Console.ReadLine());

                            if(dealer.Surname != null)
                            {
                                Console.Write("Enter Username: ");
                                dealer.Username = Convert.ToString(Console.ReadLine());

                                if(dealer.Username != null)
                                {
                                    if (UserQuery.isUsernameNotUsed(dealer.Username))
                                    {
                                        Console.Write("Enter Password: ");
                                        dealer.Password = Convert.ToString(Console.ReadLine());

                                        if (dealer.Password != null)
                                        {
                                            Console.Write("Enter Email: ");
                                            dealer.Email = Convert.ToString(Console.ReadLine());

                                            if (dealer.Email != null)
                                            {
                                                Console.Write("Enter Phone: ");
                                                dealer.TelNo = Convert.ToString(Console.ReadLine());

                                                if (dealer.TelNo != null)
                                                {
                                                    DealerQuery.addDealer(dealer);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                        break;

                    case 3:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
          


        }

        public static void dealerMenuPage(Dealer dealer)
        {
            Car tempCar = new Car();
            int flag = 1;
            while (flag == 1)
            {
                Console.WriteLine("------DEALER MENU PAGE------");
                Console.WriteLine($"Welcome {dealer.Name} {dealer.Surname}");
                Console.WriteLine("");
                Console.WriteLine("1-Sales Transactions");
                Console.WriteLine("2-Update Stocks");
                Console.WriteLine("3-Add Car");
                Console.WriteLine("4-Delete Car");
                Console.WriteLine("5-Account Information");
                Console.WriteLine("6-Delete Account");
                Console.WriteLine("7-Exit");
                Console.Write("Please enter your selection: ");

                int ansMenu = Convert.ToInt32(Console.ReadLine());
                switch (ansMenu)
                {
                    case 1:
                        DealerQuery.displaySalesRequest();
                        Console.Write("Please enter the basket ID of the sale you want to transact!: ");
                        int ansId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Do you want to sell the product?[Y/y: Yes - N/n: No]: ");
                        char ansChoose = Convert.ToChar(Console.ReadLine());

                        if (ansChoose == 'Y' || ansChoose == 'y') DealerQuery.moveToLog(ansId);
                        else DealerQuery.deleteBasket(ansId);

                        break;

                    case 2:                        
                        CarQuery.displayAndUpdateStocks();
                        break;

                    case 3:                        
                        Console.Write("Enter Car's Brand Name: ");
                        tempCar.BrandName = Console.ReadLine();
                        Console.Write("Enter Car's Model: ");
                        tempCar.Model = Console.ReadLine();
                        Console.Write("Enter Car's Packet: ");
                        tempCar.Packet = Console.ReadLine();

                        CarQuery.addCar(tempCar);
                        break;

                    case 4:
                        CarQuery.listCar();                       
                        Console.WriteLine("Please enter the ID of the car you want to delete!:");
                        int ansDelete = Convert.ToInt32(Console.ReadLine());
                        CarQuery.deleteCar(ansDelete);
                        break;

                    case 5:
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(dealer.ToString());
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("");
                        break;

                    case 6:
                        DealerQuery.deleteDealerAccount(dealer.Id);
                        break;

                    case 7:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
        }

        public static void customerMainPage()
        {
            int flag = 1;
            while (flag == 1)
            {
                Console.WriteLine("------CUSTOMER MAIN PAGE------");
                Console.WriteLine("1-Sign in");
                Console.WriteLine("2-Sign Up");
                Console.WriteLine("3-Exit");
                Console.Write("Please enter your selection: ");
                int ans = Convert.ToInt32(Console.ReadLine());
                switch (ans)
                {
                    case 1:
                        Customer customer = new Customer();
                        Console.Write("Enter the username: ");
                        String username = Console.ReadLine();

                        Console.Write("Enter the password: ");
                        String password = Console.ReadLine();
                        
                        if (CustomerQuery.loginCustomer(username, password, customer)) customerMenuPage(customer);
                        else Console.WriteLine("Your username or password is wrong!");
                        break;

                    case 2:
                        Customer tempCustomer = new Customer();

                        Console.Write("Enter name: ");
                        tempCustomer.Name = Convert.ToString(Console.ReadLine());

                        if (tempCustomer.Name != null)
                        {
                            Console.Write("Enter Surname: ");
                            tempCustomer.Surname = Convert.ToString(Console.ReadLine());

                            if (tempCustomer.Surname != null)
                            {
                                Console.Write("Enter Username: ");
                                tempCustomer.Username = Convert.ToString(Console.ReadLine());

                                if (tempCustomer.Username != null)
                                {
                                    if (UserQuery.isUsernameNotUsed(tempCustomer.Username))
                                    {
                                        Console.Write("Enter Password: ");
                                        tempCustomer.Password = Convert.ToString(Console.ReadLine());

                                        if (tempCustomer.Password != null)
                                        {
                                            Console.Write("Enter Email: ");
                                            tempCustomer.Email = Convert.ToString(Console.ReadLine());

                                            if (tempCustomer.Email != null)
                                            {
                                                Console.Write("Enter Phone: ");
                                                tempCustomer.TelNo = Convert.ToString(Console.ReadLine());

                                                if (tempCustomer.TelNo != null)
                                                {
                                                    CustomerQuery.addCustomer(tempCustomer);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        break;

                    case 3:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
        }

        public static void customerMenuPage(Customer customer)
        {
            int flag = 1;
            while(flag == 1)
            {
                Console.WriteLine("------CUSTOMER MENU PAGE------");
                Console.WriteLine($"Welcome {customer.Name} {customer.Surname}");
                Console.WriteLine("");
                Console.WriteLine("1-List Cars");
                Console.WriteLine("2-Account Information");
                Console.WriteLine("3-Delete Account");
                Console.WriteLine("4-Exit");
                Console.Write("Please enter your selection: ");
                int ans = Convert.ToInt32(Console.ReadLine());

                switch (ans)
                {
                    case 1:
                        Car tempCar = new Car();
                        CarQuery.listCar();
                        Console.Write("Please enter the ID of the car you want to buy spare parts for: ");
                        tempCar.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("1-Engine");
                        Console.WriteLine("2-Clutch");
                        Console.WriteLine("3-Exhaust");
                        Console.WriteLine("4-Brakes");
                        Console.WriteLine("5-Battery");
                        Console.Write("Please enter the number of spare part you want to buy![1-5]: ");
                        int ansSpareParts = Convert.ToInt32(Console.ReadLine());
                        string sparePart;
                        if(ansSpareParts == 1)
                        {
                            sparePart = "engine";
                            CustomerQuery.addToBasket(sparePart, customer.Id,tempCar.Id);
                        }
                        else if(ansSpareParts == 2)
                        {
                            sparePart = "clutch";
                            CustomerQuery.addToBasket(sparePart, customer.Id, tempCar.Id);
                        }
                        else if(ansSpareParts == 3)
                        {
                            sparePart = "exhaust";
                            CustomerQuery.addToBasket(sparePart, customer.Id, tempCar.Id);
                        }
                        else if(ansSpareParts == 4)
                        {
                            sparePart = "brakes";
                            CustomerQuery.addToBasket(sparePart, customer.Id, tempCar.Id);
                        }
                        else if (ansSpareParts == 5)
                        {
                            sparePart = "battery";
                            CustomerQuery.addToBasket(sparePart, customer.Id, tempCar.Id);
                        }
                        else Console.WriteLine("Wrong Entry!");
                        break;

                    case 2:
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(customer.ToString());
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("");
                        break;

                    case 3:
                        if (CustomerQuery.deleteCustomerAccount(customer.Id))
                        {                            
                            customerMainPage();
                        }
                        break;

                    case 4:
                        flag = 0;
                        break;

                    default:
                        Console.WriteLine("Wrong Entry!");
                        break;
                }
            }
        }
    }
}
