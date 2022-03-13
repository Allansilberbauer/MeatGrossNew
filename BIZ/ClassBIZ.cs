using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using IO;
namespace BIZ
{
    /// <summary>
    /// Denne klasse repræsenter forretningslogikken i applikationen
    /// </summary>
    public class ClassBIZ : ClassNotify
    {
        ClassJsonWepApiCall cowa = new ClassJsonWepApiCall();
        ClassClassMeatGrossDB cmgdb = new ClassClassMeatGrossDB(); 

        private bool _isEnabledRight;
        private bool _isEnabledLeft;
        private ClassOrder _order;
        private ClassCustomer _editOrnewCustomer;
        private ClassCustomer _SelectedCustomer;
        private ClassApiRates _apiRates;
        private List<ClassMeat> _listMeat;
        private List<ClassCountry> _listCountry;
        private List<ClassCustomer> _listCustomer;
        public ClassBIZ() 
        {
            isEnabledRight = false;
            isEnabledLeft = true;
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
        
        /// <summary>
        /// holder alle Customer fra databasen
        /// </summary>
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
        /// <summary>
        /// holder alle lande fra databasen
        /// </summary>
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
        /// <summary>
        /// holder alla kødtyper fra databasen
        /// </summary>
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

        /// <summary>
        /// holder Customer dataen af den valgte købere.
        /// </summary>
        public ClassCustomer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set
            {
                if (_SelectedCustomer != value)
                {
                    _SelectedCustomer = value;

                    //checker om der er blevet selected en customer
                    if (SelectedCustomer != null && SelectedCustomer.id > 0)
                    {
                        order.orderCustomer = SelectedCustomer;   //overføre en indstands af SelectedCustomer over i order.orderCustomer
                        isEnabledRight = true;
                    }                 
                }
                Notify("SelectedCustomer");
            }
        }

        /// <summary>
        /// midlertidig holding af customer data til redigering og oprætning af købere
        /// </summary>
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

        /// <summary>
        /// holder dataen til håntering og skabning af ordre
        /// </summary>
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

        /// <summary>
        /// kontrolere om højre UserControl er enabled eller ikke 
        /// </summary>
        public bool isEnabledRight
        {
            get { return _isEnabledRight; }
            set
            {
                if (_isEnabledRight != value)
                {
                    _isEnabledRight = value;
                }
                Notify("isEnabledRight");
            }
        }
        /// <summary>
        /// kontrolere om venstre UserControl er enabled eller ikke 
        /// </summary>
        public bool isEnabledLeft
        {
            get { return _isEnabledLeft; }
            set
            {
                if (_isEnabledLeft != value)
                {
                    _isEnabledLeft = value;
                }
                Notify("isEnabledLeft");
            }
        }


        /// <summary>
        /// opdatere listCustomer med alle Customer fra databasen 
        /// </summary>
        public void UpdateListCustomer()
        {
            listCustomer = cmgdb.GetAllCustomerFromDB();
        }
        /// <summary>
        /// gør et kald på GetRatesFromWebApi() i IO og returner dets return værdi
        /// </summary>
        /// <returns>ClassApiRates</returns>
        public async Task<ClassApiRates> GetApiRates()
        {
            ClassApiRates res = new ClassApiRates();

            res = await cowa.GetRatesFromWebApi();

            return res;
        }
        /// <summary>
        /// opdatere listCustomer med alle Customer fra databasen og 
        /// opdatere listMeat med alle kødtyper fra databasen
        /// </summary>
        public void SetUpListCustomer()
        {
            listCustomer = cmgdb.GetAllCustomerFromDB();
            listMeat = cmgdb.GetAllCMeatFromDB();
        }
        /// <summary>
        /// tilføger ny customer til databasen (baseret på hvad der står i editOrnewCustomer) og der efter initialisere SelectedCustomer til den nye customer
        /// </summary>
        /// <returns>int</returns>
        public int SaveNewCustomer()
        {
            int newID = cmgdb.SaveNewCustomerInDB(editOrnewCustomer);
            UpdateListCustomer();
            foreach (ClassCustomer customer in listCustomer)
            {
                if (customer.id == newID)
                {
                    SelectedCustomer = customer;
                }
            }
            return newID;
        }
        /// <summary>
        /// opdatere den valte customer (baseret på hvad der står i editOrnewCustomer) til databasen og SelectedCustomer
        /// </summary>
        public void UpdateCustomer()
        {
            cmgdb.UpdateCustomerInDB(editOrnewCustomer);

            SelectedCustomer.contactName = editOrnewCustomer.contactName;
            SelectedCustomer.companyName = editOrnewCustomer.companyName;
            SelectedCustomer.address = editOrnewCustomer.address;
            SelectedCustomer.zipCity = editOrnewCustomer.zipCity;
            SelectedCustomer.mail = editOrnewCustomer.mail;
            SelectedCustomer.phone = editOrnewCustomer.phone;
            SelectedCustomer.country = editOrnewCustomer.country;

            editOrnewCustomer = new ClassCustomer();
        } 

        public void SaveSaleInDB()
        {

        }
        public void SaveNewMeatPrice(string inMeat, double inPrice, int inWeight)
        {

        }

        /// <summary>
        /// opdatere listCountry med alle lande fra database
        /// </summary>
        private void SetUpListCountry()
        {
            listCountry = cmgdb.GetAllCountryFromDB();
        }
    }
}
