using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AraçSatisYazilimi.Vehicle
{
    public class Car
    {
        private string _id;
        private string _brandname;
        private string _model;
        private string _packet;
        private int _engine;
        private int _clutch;
        private int _exhaust;
        private int _brakes;
        private int _battery;

        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string Packet { get; set; }
        public int Engine { get; set; }
        public int Clutch { get; set; }
        public int Exhaust { get; set; }
        public int Brakes { get; set; }
        public int Battery { get; set; }


        public Car()
        {

        }

        public Car(int id, string brandName, string model, string packet, int engine, int clutch, int exhaust, int brakes, int battery)
        {
            Id = id;
            BrandName = brandName;
            Model = model;
            Packet = packet;
            Engine = engine;
            Clutch = clutch;
            Exhaust = exhaust;
            Brakes = brakes;
            Battery = battery;
        }
    }
}
