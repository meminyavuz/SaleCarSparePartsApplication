using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.NewFolder
{
    public class Dealer:User
    {
        public Dealer()
        {

        }
        public Dealer(int id, string name, string surname, string username, string password, string email, string telno) : base(id, name, surname, username, password, email, telno)
        {

        }

        public override string ToString()
        {
            return $"Dealer ID: {Id} \nDealer Name: {Name}\nDealer Surname: {Surname}\nDealer Username: {Username}\nDealer Password: {Password}\nDealer Email: {Email}\nDealer Phone: {TelNo}";
        }
    }
}
