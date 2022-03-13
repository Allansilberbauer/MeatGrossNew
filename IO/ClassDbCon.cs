using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Repository;
namespace IO
{
    public class ClassDbCon
    {
        private string _connectionString;
        protected SqlConnection con;
        private SqlCommand _command;

        /// <summary>
        /// default constructor med fast angivelse af connectionstring.
        /// </summary>
        public ClassDbCon()
        {
             _connectionString = @"";
             con = new SqlConnection(_connectionString);
        }
        /// <summary>
        /// overloaded constructor hvor connectionstring initialiseres via en overført parameter
        /// </summary>
        /// <param name="inConnectionString">string</param>
        public ClassDbCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// metode der kun er tilgængelig via nedarving.
        /// metoden har til formål at gøre det muligt at initialisere connectionstring under
        /// afvikling af programmet og sætte connectionstring efter behov.
        /// </summary>
        /// <param name="inConnectionString">string</param>
        protected void setCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// denne metode åbner forbindelsen til databasen.
        /// den undersøger om de gængse betingelser er opfyldt for at åbne forbindelsen inden den åbnes.
        /// hvis betingelserne ikke er opfyldt, vil den prøve at håndtere de mest almindelige fejl og mangler
        /// </summary>
        protected void OpenDB()
        {
            try
            {
                if (this.con != null && this.con.State == ConnectionState.Closed)
                {
                    this.con.Open();// åbner forbindelsen til DB

                }
                else
                {
                    if (this.con.State == ConnectionState.Open)  // undersøger om fejlen skyldes at der er en åben forbindelse i forvejen
                    {
                        // hvis ja - lukker den forbindelsen og kalder "sig selv" igen for at åbne forbindelsen.
                        CloseDB();
                        OpenDB();
                    }
                    else  // hvis det ikke var på grund af en åben forbindelse må det være på grund af manglende initialisering af "con" 
                    {
                        this.con = new SqlConnection(_connectionString); // initialisere "con" med den angivne connectionstring
                        OpenDB(); // kalder "sig selv" igen for at åbne forbindelsen 
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// denne metode lukker forbindelsen til DB
        /// </summary>
        protected void CloseDB()
        {
            try
            {
                this.con.Close(); //lukker forbindelsen
            }
            catch (SqlException ex)  // håntere de exceptions (fejl) der måtte opstå under kommunikation med databasen 
            {

                throw ex;
            }
        }

        /// <summary>
        /// denne metode har til formål, at udføre de handlinger i databasen, som ikke kræver at der retuneres et resultatsæt.
        /// metoden vil dog altid retunere en intiger værdi der angiver om handlingen gik godt eller skidt.
        /// retuneres: -1 er handlingen ikke blevet udført
        /// retuneres: et tal fre 0 til X, indikerer det at udtrykket kunne eksekveres og angiver hvor mange datasæt der 
        /// blev påvirket
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>int</returns>
        protected int ExecuteNonQuery(string sqlQuery)
        {
            int res = 0;
            
            

            try
            {
                OpenDB();// åbner forbindelsen til DB

                // her initialiseres instansen af SqlCommand med parameterne string "sqlQuery" og SqlConnection "con"
                using (_command = new SqlCommand(sqlQuery, con))
                {
                    res = _command.ExecuteNonQuery(); // her kaldes databasen og den givne query eksekveres 
                }

            }
            catch (SqlException ex) // håntere de exceptions (fejl) der måtte opstå under kommunikation med databasen 
            {

                throw ex;
            }
            // ved angivelse "finally" sikre jeg at det der står i "finally" altid bliver
            // udført, uanset om koden kunne eksekveres med eller uden fejl
            finally
            {
                CloseDB();// lukker forbindelsen til DB
            }

            return res;
        }

        /// <summary>
        /// denne metode skal håndtere forespøgelser til databasen som skal returnere et resultatsæt.
        /// det resultatsæt der modtages fra DB, konverteres over i en collection af typen DataTable
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>DataTable</returns>
        protected DataTable DbReturnDataTable(string sqlQuery) 
        {
            DataTable dtRes = new DataTable();

            try
            {
                OpenDB();
                // her initialiseres instansen af SqlCommand med paramterne string query og SqlConnection con
                using (_command = new SqlCommand(sqlQuery, con))
                {
                    // her foretages kaldet til databasen ved at der oprettes en ny instands af en SqlDataAdapter.
                    using (SqlDataAdapter adapter = new SqlDataAdapter(_command))
                    {
                        adapter.Fill(dtRes); // her transformeres data i "adapter" til formatet DataTable som er 
                                             // er mere anvendeligt i C# koden der skal modtage resultatet af forespørgelsen.
                    }
                }
            }
            catch (SqlException ex)  // håntere de exceptions (fejl) der måtte opstå under kommunikation med databasen 
            {

                throw ex;
            }
            // ved angivelse "finally" sikre jeg at det der står i "finally" altid bliver
            // udført, uanset om koden kunne eksekveres med eller uden fejl
            finally
            {
                CloseDB();
            }
            return dtRes;
        }


        /// <summary>
        /// denne metode skal håndtere forespøgelser til databasen som skal retunere en tekststreng.
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>string</returns>
        protected string DbReturnString(string sqlQuery)
        {
            string res = "";
            bool foundData = false;

            try
            {
                OpenDB();

                //opretter en ny instans af SqlCommand med Parameterne sqlQuery og con,
                //som indeholer henholdsvis min sql forspøgelse og information omkring
                //hvilken database data skal hentes fra.
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    //her eksikveres forespøgelsen på databasen og svaret gammes i reader som er af datatypen
                    //SqlDataReader der har samme egenskaber som en StraemReader, altså egenskaber der gør den 
                    //egnet til at modtage og holde en stream af teskt
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // hvis reader har modtaget et resultat fra databasen, skal den udføre koden i while loopet
                        while (reader.Read())
                        {
                            res = reader.GetString(0);// læser teksten fra reader og indsætter den i res.
                                                        // tallet 0 angiver hvorfra i teksten der skal læses, 0 = start.

                            foundData = true;  // bolsk værdi, der angiver at der er modtaget et resultat
                        }
                        // hvis der ikke findes et resultat i databasen skal der returneres en hjælpetekst.
                        if (!foundData)
                        {
                            res = "Der blev ikke fundet nogen data";
                        }
                    }
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

        /// <summary>
        /// dene metode skal håndtere forespørgelser til databasen som skal returnere et resultatsæt.
        /// forespørgelsen skal foretages gennem en storedProcedure på SqlServeren.
        /// det resultatsæt der modtages fra DB, konverteres over i en collection af typen DataTable
        /// </summary>
        /// <param name="inCommand"></param>
        /// <returns></returns>
        protected DataTable MakeCallToStoredProcedure(SqlCommand inCommand)
        {
            DataTable DtRes = new DataTable();

            try
            {
                OpenDB();  // åbner forbindelsen til databasen

                // her initialisere en instans af SqldataAdapter med værdien i inCommand
                using (SqlDataAdapter adapter = new SqlDataAdapter(inCommand)) 
                {
                    adapter.Fill(DtRes); // her overføres data fra adapter til den DataTable, metoden skal returnere. 
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();   //lukker forbindelsen til databasen
            }

            return DtRes;
        }


    }
}
