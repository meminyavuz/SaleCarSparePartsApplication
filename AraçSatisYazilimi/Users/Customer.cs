using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.NewFolder
{
    public class Customer:User
    {
        public Customer()
        {

        }
        public Customer(int id, string name, string surname, string username, string password, string email, string telno) : base(id, name, surname, username, password, email, telno)
        {

        }
        public override string ToString()
        {
            return $"Customer ID: {Id} \nCustomer Name: {Name}\nCustomer Surname: {Surname}\nCustomer Username: {Username}\nCustomer Password: {Password}\nCustomer Email: {Email}\nCustomer Phone: {TelNo}";
        }
    }
}
