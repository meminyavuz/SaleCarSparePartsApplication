using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AraçSatisYazilimi
{
    public class User
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private string _email;
        private string _phone;
        public int Id { 
            get 
            { 
                return _id;  }
            set
            {
                _id = value;
            } 
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {

                if (char.IsLetter(value[0]))
                {
                    if (IsString(value))
                    {
                        _name = value;
                    }
                    else
                    {
                        Console.WriteLine("Your name must only consist of letters!");
                        _name = null;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Your name must start with a letter!");
                    _name = null;
                }
            }

        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (char.IsLetter(value[0]))
                {
                    if (IsString(value))
                    {
                        _surname = value;
                    }
                    else
                    {
                        Console.WriteLine("Your surname must only consist of letters!");
                        _surname = null;
                    }
                }
                else
                {
                    Console.WriteLine("Your surname must start with a letter!");
                    _surname = null;
                }
            }
        }
        public string Username { 
            get 
            {
                return _username;
            }
            set
            {
                if (value.Length > 5 && value.Length < 20)
                {
                    foreach (char c in value)
                    {
                        if (char.IsLetterOrDigit(c)) _username = value;
                        else
                        { 
                            Console.WriteLine("Username must consist of numbers or characters!");
                            _username = null;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Username must be between 5 and 20 characters!");
                    _username = null;
                }
            }
        }
        public string Password { 
            get 
            { 
                return _password; 
            }
            set
            {
                if (value.Length > 8 && value.Length < 20)
                {
                    if (value.Any(char.IsDigit) && value.Any(char.IsUpper) && value.Any(char.IsLower) && value.Any(c => "!@#$%&*-+".Contains(c)) && !value.Contains(" "))
                    {
                        _password = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password!");
                        Console.WriteLine("Your password must contain:");
                        Console.WriteLine("*Contains at least one digit.\n*Contains at least one uppercase alphabet.\n*Contains at least one lowercase alphabet.\n*Contains at least one special character consisting of !@#$%&*-+.\n*Does not contain any spaces.");
                        _password = null;
                    }
                }
                else
                {
                    Console.WriteLine("Password must be between 8 and 20 characters!");
                    _password = null;
                }
            }
           
        }
        public string Email { 
            get 
            { 
                return _email;
            }
            set
            {
                if (char.IsLetterOrDigit(value[0]) && value.Contains("@"))
                {
                    _email = value;
                }
                else
                {
                    Console.WriteLine("Invalid E-Mail![Email cannot be initialized with special characters and must contain @ symbol.]");
                    _email = null;
                }
            }
        }
        public string TelNo { 
            get 
            { 
                return _phone;
            }
            set
            {
                if (value.Length == 10)
                {
                    foreach (char c in value)
                    {
                        if (char.IsDigit(c)) _phone = value;
                        else
                        {
                            Console.WriteLine("Invalid Phone No![Phone Number must consist of numbers only]");
                            _phone = null;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Phone No![Phone Number must be in the form xxx-xxx-xxxx.]");
                    _phone = null;
                }
            }
        }

        public User()
        {

        }
        public User(int id, string name,string surname, string username, string password,string email, string telno)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            Email = email;
            TelNo = telno;
        }

        static bool IsString(string username)
        {
            foreach (char c in username)
            {
                
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
