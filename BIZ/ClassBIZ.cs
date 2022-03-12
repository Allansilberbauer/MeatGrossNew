﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using IO;
namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
        ClassJsonWepApiCall cowa = new ClassJsonWepApiCall();
        ClassClassMeatGrossDB cmgdb = new ClassClassMeatGrossDB(); 

        private bool _isEnabled;
        private ClassOrder _order;
        private ClassCustomer _editOrnewCustomer;
        private ClassCustomer _SelectedCustomer;
        private ClassApiRates _apiRates;
        private List<ClassMeat> _listMeat;
        private List<ClassCountry> _listCountry;
        private List<ClassCustomer> _listCustomer;
        public ClassBIZ() 
        {
            isEnabled = true;
            order = new ClassOrder();
            editOrnewCustomer = new ClassCustomer();
            SelectedCustomer = new ClassCustomer();
            apiRates = new ClassApiRates();
            listMeat = new List<ClassMeat>();
            listCountry = new List<ClassCountry>();
            listCustomer = new List<ClassCustomer>();

            SetUpListCustomer();
            SetUpListCountry();
        }
        

        public List<ClassCustomer> listCustomer
        {
            get { return _listCustomer; }
            set
            {
                if (_listCustomer != value)
                {
                    _listCustomer = value;
                }
                Notify("listCustomer");
            }
        }

        public List<ClassCountry> listCountry
        {
            get { return _listCountry; }
            set
            {
                if (_listCountry != value)
                {
                    _listCountry = value;
                }
                Notify("listCountry");
            }
        }

        public List<ClassMeat> listMeat
        {
            get { return _listMeat; }
            set
            {
                if (_listMeat != value)
                {
                    _listMeat = value;
                }
                Notify("listMeat");
            }
        }

        public ClassApiRates apiRates
        {
            get { return _apiRates; }
            set
            {
                if (_apiRates != value)
                {
                    _apiRates = value;
                }
                Notify("apiRates");
            }
        }

        public ClassCustomer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set
            {
                if (_SelectedCustomer != value)
                {
                    _SelectedCustomer = value;
                }
                Notify("SelectedCustomer");
            }
        }

        public ClassCustomer editOrnewCustomer
        {
            get { return _editOrnewCustomer; }
            set
            {
                if (_editOrnewCustomer != value)
                {
                    _editOrnewCustomer = value;
                }
                Notify("editOrnewCustomer");
            }
        }

        public ClassOrder order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                }
                Notify("order");
            }
        }

        public bool isEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                }
                Notify("isEnabled");
            }
        }



        public void UpdateListCustomer()
        {

        }
        public async Task<ClassApiRates> GetApiRates()
        {
            ClassApiRates res = new ClassApiRates();

            res = await cowa.GetRatesFromWebApi();

            return res;
        }
        public void SetUpListCustomer()
        {
            listCustomer = cmgdb.GetAllCustomerFromDB();           
        }
        public  int SaveNewCustomer()
        {
            int res = 0;

            return res;
        }
        public void UpdateCustomer()
        {

        } 
        public void SaveSaleInDB()
        {

        }
        public void SaveNewMeatPrice(string inMeat, double inPrice, int inWeight)
        {

        }

        private void SetUpListCountry()
        {
            listCountry = cmgdb.GetAllCountryFromDB();
        }
    }
}