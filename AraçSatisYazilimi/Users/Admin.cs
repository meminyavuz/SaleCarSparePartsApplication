using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.NewFolder
{
    public class Admin : User
    {
        public Admin()
        {

        }
        public Admin(int id, string name, string surname, string username, string password, string email, string telno) : base(id, name, surname, username, password, email, telno)
        {
            
        }
        public override string ToString()
        {
            return $"Admin ID: {Id} \nAdmin Name: {Name}\nAdmin Surname: {Surname}\nAdmin Username: {Username}\nAdmin Password: {Password}\nAdmin Email: {Email}\nAdmin Phone: {TelNo}";
        }

    }
}
