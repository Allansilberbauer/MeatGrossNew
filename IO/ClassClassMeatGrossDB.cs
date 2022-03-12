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

            try
            {
                DataTable dt = DbReturnDataTable("SELECT * FROM Meat ");
                foreach (DataRow row in dt.Rows)
                {
                    ClassMeat cm = new ClassMeat();

                    cm.id = Convert.ToInt32(row["id"]);
                    cm.typeOfMeat = row["TypeOfMeat"].ToString();
                    cm.stock = (int)row["Stock"];                    
                    cm.price = Convert.ToDouble(row["Price"]);
                    cm.priceTimestamp = Convert.ToDateTime(row["PriceTimeStamp"]);

                    res.Add(cm);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }
        public int SaveNewCustomerInDB(ClassCustomer inClassCustomer)
        {
            int res = 0;
           
            string sqlQuery = "INSERT INTO Customer " +
                "(ContactName, Addres, ZipCity, Mail, Phone, ComanyName, Country) " +
                "VALUES " +
                "(@ContactName, @address, @zipCity, @mail, @phone, @ComanyName, @Country) " +
                "SELECT SCOPE_IDENTITY()";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = inClassCustomer.contactName;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inClassCustomer.address;
                    cmd.Parameters.Add("@zipCity", SqlDbType.NVarChar).Value = inClassCustomer.zipCity;
                    cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = inClassCustomer.mail;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inClassCustomer.phone;
                    cmd.Parameters.Add("@ComanyName", SqlDbType.NVarChar).Value = inClassCustomer.companyName;
                    cmd.Parameters.Add("@Country", SqlDbType.Int).Value = inClassCustomer.country.id;
                    

                    OpenDB();
                    res = Convert.ToInt32(cmd.ExecuteScalar()); 
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }

            return res;
        }
        public void UpdateCustomerInDB(ClassCustomer inClassCustomer)
        {
            
            string sqlQuery = "UPDATE Customer " +
                "SET ContactName = @ContactName, " +
                "Addres = @address, " +
                "ZipCity = @zipCity, " +
                "Mail = @mail," +
                "Phone = @phone, " +
                "ComanyName = @ComanyName, " +
                "Country = @Country " +               
                "WHERE id = @id ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = inClassCustomer.contactName;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inClassCustomer.address;
                    cmd.Parameters.Add("@zipCity", SqlDbType.NVarChar).Value = inClassCustomer.zipCity;                  
                    cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = inClassCustomer.mail;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inClassCustomer.phone;
                    cmd.Parameters.Add("@ComanyName", SqlDbType.NVarChar).Value = inClassCustomer.companyName;
                    cmd.Parameters.Add("@Country", SqlDbType.Int).Value = inClassCustomer.country.id;                   
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inClassCustomer.id;

                    OpenDB();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }
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
