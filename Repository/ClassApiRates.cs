using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassApiRates 
    {
        private long _timestamp;
        private Dictionary<string, double> _rates;
        private string _newTimestamp;

        public ClassApiRates()
        {
            timestamp = 0;
            newTimestamp = "0";
            rates = new Dictionary<string, double>();
        }
        
        public string newTimestamp
        {
            get { return _newTimestamp; }
            set { _newTimestamp = value; }           
        }

        public Dictionary<string, double> rates
        {
            get { return _rates; }
            set
            {              
                 _rates = value;               
            }
        }

        public long timestamp
        {
            get { return _timestamp; }
            set
            {               
                 _timestamp = value;            
            }
        }

    }
}
