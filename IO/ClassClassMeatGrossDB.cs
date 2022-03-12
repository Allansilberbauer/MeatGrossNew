using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace IO
{
    public class ClassClassMeatGrossDB : ClassDbCon
    {
        public ClassClassMeatGrossDB()
        {
            setCon(@"Server=(localdb)\MSSQLLocalDB;Database=MeatGrossDB;Trusted_Connection=True;");
        }

        public List<ClassCustomer> GetAllCustomerFromDB()
        {
            List<ClassCustomer> res = new List<ClassCustomer>();
            string sqlQuery = "SELECT Customer.id, Customer.ComanyName, Customer.Addres, Customer.ZipCity, Customer.Phone, Customer.Mail, Customer.ContactName, CountryAndRates.CountryCode, "+
                  "CountryAndRates.CountryName, CountryAndRates.ValutaName, CountryAndRates.ValutaRate, CountryAndRates.UpdateTime, Customer.Country " +
                  "FROM   Customer LEFT OUTER JOIN CountryAndRates ON Customer.Country = CountryAndRates.id";
                  
            try
            {
                DataTable dt = DbReturnDataTable(sqlQuery);
                foreach (DataRow row in dt.Rows)
                {
                    ClassCustomer cc = new ClassCustomer();
                    ClassCountry co = new ClassCountry();

                    cc.id = Convert.ToInt32(row["id"]);
                    cc.contactName = row["ContactName"].ToString();
                    cc.address = row["Addres"].ToString();
                    cc.zipCity = row["ZipCity"].ToString();                  
                    cc.mail = row["Mail"].ToString();
                    cc.phone = row["Phone"].ToString();
                    cc.companyName = row["ComanyName"].ToString();


                    co.id = (int)row["Country"];
                    co.countryCode = row["CountryCode"].ToString();
                    co.countryName = row["CountryName"].ToString();
                    co.valutaName = row["ValutaName"].ToString();
                    co.valutaRate = Convert.ToDouble(row["ValutaRate"]);
                    co.updateTime = Convert.ToDateTime(row["UpdateTime"]);

                    cc.country = co;

                    res.Add(cc);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }
        public List<ClassCountry> GetAllCountryFromDB()
        {
            List<ClassCountry> res = new List<ClassCountry>();

            try
            {
                DataTable dt = DbReturnDataTable("SELECT * FROM CountryAndRates ");
                foreach (DataRow row in dt.Rows)
                {
                    ClassCountry cc = new ClassCountry();

                    cc.id = Convert.ToInt32(row["id"]);
                    cc.countryCode = row["CountryCode"].ToString();
                    cc.countryName = row["CountryName"].ToString();
                    cc.valutaName = row["ValutaName"].ToString();
                    cc.valutaRate = Convert.ToDouble(row["ValutaRate"]);
                    cc.updateTime = Convert.ToDateTime(row["UpdateTime"]); 

                    res.Add(cc);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }
        public List<ClassMeat> GetAllCMeatFromDB()
        {
            List<ClassMeat> res = new List<ClassMeat>();

            return res;
        }
        public int SaveNewCustomerInDB(ClassCustomer inClassCustomer)
        {
            int res = 0;

            return res;
        }
        public void UpdateCustomerInDB(ClassCustomer inClassCustomer)
        {

        }
        public void UpdateMeatVolume(ClassOrder inOrder)
        {

        }
        public void UpdatePriceForMeatInDB(string inMeat, double inPrice, int inWeight)
        {

        }
        public void UpdateTimestampForMeat()
        {

        }
        public int AddOrderToDB(ClassOrder inOrder)
        {
            int res = 0;

            return res;
        }
    }
}
