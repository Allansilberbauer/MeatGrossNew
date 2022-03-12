using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCountry : ClassNotify
    {
        private int _id;
        private string _countryCode;
        private string _countryName;
        private string _valutaName;
        private double _valutaRate;
        private DateTime _updateTime;

        public ClassCountry()
        {
            id = 0;
            countryName = "";
            countryCode = "";
            valutaName = "";
            valutaRate = 0D;
            updateTime = new DateTime();
        }
        public ClassCountry(ClassCountry inClassCountry)
        {

        }

        public DateTime updateTime
        {
            get { return _updateTime; }
            set
            {
                if (_updateTime != value)
                {
                    _updateTime = value;
                }
                Notify("updateTime");
            }
        }

        public double valutaRate
        {
            get { return _valutaRate; }
            set
            {
                if (_valutaRate != value)
                {
                    _valutaRate = value;
                }
                Notify("valutaRate");
            }
        }

        public string valutaName
        {
            get { return $"{_valutaName} ({_countryName}) "; }
            set
            {
                if (_valutaName != value)
                {
                    _valutaName = value;
                }
                Notify("valutaName");
            }
        }

        public string countryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName != value)
                {
                    _countryName = value;
                }
                Notify("counryName");
            }
        }

        public string countryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode != value)
                {
                    _countryCode = value;
                }
                Notify("countryCode");
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
