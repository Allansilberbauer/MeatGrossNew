using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassOrder : ClassNotify
    {
        public ClassOrder()
        {

        }
        private string _priceValuta;
        private string _priceDKK;
        private double _orderPriceValuta;
        private double _orderPriceDKK;
        private int _orderWeight;
        private ClassCustomer _orderCustomer;
        private ClassMeat _orderMeat;

        public ClassMeat orderMeat
        {
            get { return _orderMeat; }
            set
            {
                if (_orderMeat != value)
                {
                    _orderMeat = value;

                    //tjekker at der er valgt en Customer inden der bliver regnet på prisen
                    if (orderCustomer != null)
                    {
                        CalculateAllPrices();
                    }
                }
                Notify("orderMeat");
            }
        }
        public ClassCustomer orderCustomer
        {
            get { return _orderCustomer; }
            set
            {
                if (_orderCustomer != value)
                {
                    _orderCustomer = value;

                    //tjekker at der er valgt en kødtype inden der bliver regnet på prisen
                    if (orderMeat != null)
                    {
                        CalculateAllPrices();
                    }
                }
                Notify("orderCustomer");
            }
        }
        public int orderWeight
        {
            get { return _orderWeight; }
            set
            {
                if (_orderWeight != value)
                {
                    //tjekker at både Customer og kødtype er blevet valgt inden der kan skrives ned i orderWeight
                    if (orderCustomer != null && orderMeat != null)
                    {                                                
                        if (value > orderMeat.stock)
                        {
                            _orderWeight = orderMeat.stock;
                        }
                        else
                        {
                            _orderWeight = value;
                        }
                        CalculateAllPrices();                         
                    }
                }               
                Notify("orderWeight");
            }
        }
        public double orderPriceDKK
        {
            get { return _orderPriceDKK; }
            set
            {
                if (_orderPriceDKK != value)
                {
                    _orderPriceDKK = value;
                }
                Notify("orderPriceDKK");
            }
        }
        public double orderPriceValuta
        {
            get { return _orderPriceValuta; }
            set
            {
                if (_orderPriceValuta != value)
                {
                    _orderPriceValuta = value;
                }
                Notify("orderPriceValuta");
            }
        }

        /// <summary>
        /// holder prisen i Danske Kroner til binding i GUI
        /// </summary>
        public string priceDKK
        {
            get { return _priceDKK; }
            set
            {
                if (_priceDKK != value)
                {
                    _priceDKK = value;
                }
                Notify("priceDKK");
            }
        }
        /// <summary>
        /// holder prisen i kundens valte valuta til binding i GUI
        /// </summary>
        public string priceValuta
        {
            get { return _priceValuta; }
            set
            {
                if (_priceValuta != value)
                {
                    _priceValuta = value;
                }
                Notify("priceValuta");
            }
        }

        /// <summary>
        /// udregner prisen af Kundens køb i danske kroner og kundens valte valuta
        /// </summary>
        private void CalculateAllPrices()
        {
            int orderAmount = orderWeight;
            double pricePerUnitDKK = orderMeat.price;


            orderPriceDKK = orderAmount * pricePerUnitDKK;
            priceDKK = (orderAmount * pricePerUnitDKK).ToString();

            orderPriceValuta = (pricePerUnitDKK * orderCustomer.country.valutaRate) * orderAmount;
            priceValuta = ((pricePerUnitDKK * orderCustomer.country.valutaRate) * orderAmount).ToString();
        }
    }
}
