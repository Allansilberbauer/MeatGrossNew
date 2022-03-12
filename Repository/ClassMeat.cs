using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassMeat : ClassNotify
    {
        private int _id;
        private string _typeOfMeat;
        private int _stock;
        private double _price;
        private DateTime _priceTimestamp;
        private string _strTimestamp;

        public ClassMeat()
        {
            id = 0;
            typeOfMeat = "";
            stock = 0;
            price = 0D;
            priceTimestamp = new DateTime();
            strTimestamp = "";
        }
        

        public string strTimestamp
        {
            get { return priceTimestamp.ToLongDateString(); }
            set
            {
                if (_strTimestamp != value)
                {
                    _strTimestamp = value;
                }
                Notify("strTimestemp");
            }
        }

        public DateTime priceTimestamp
        {
            get { return _priceTimestamp; }
            set
            {
                if (_priceTimestamp != value)
                {
                    _priceTimestamp = value;
                }
                Notify("priceTimestamp");
            }
        }

        public double price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
                Notify("price");
            }
        }

        public int stock
        {
            get { return _stock; }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                }
                Notify("stock");
            }
        }

        public string typeOfMeat
        {
            get { return _typeOfMeat; }
            set
            {
                if (_typeOfMeat != value)
                {
                    _typeOfMeat = value;
                }
                Notify("typeOfMeat");
            }
        }

        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                Notify("id");
            }
        }

    }
}
