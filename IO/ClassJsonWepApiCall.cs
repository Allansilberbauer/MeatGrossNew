using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Repository;
using Newtonsoft.Json;
using System.Windows;

namespace IO
{
    public class ClassJsonWepApiCall
    {
        public ClassJsonWepApiCall()
        {

        }

        public async Task<ClassApiRates> GetRatesFromWebApi()
        {
            ClassApiRates res = new ClassApiRates();

            try
            {
                while (true)
                {
                    string strJson = await GetURLContentsAsync("https://openexchangerates.org/api/latest.json?app_id=815cc079ffe94c1588c276bb286366a8&base=USD");
                    res = JsonConvert.DeserializeObject<ClassApiRates>(strJson);

                    await Task.Delay(600000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "API Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }

        private async Task<string> GetURLContentsAsync(string inURL)
        {
            string res = "";
            MemoryStream content = new MemoryStream();
            var webReq = (HttpWebRequest)WebRequest.Create(inURL);

            try
            {
                using (WebResponse response = await webReq.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
                res = Encoding.UTF8.GetString(content.ToArray());
            }
            catch (IOException ex)
            {

                throw ex;
            }
            finally
            {
                content.Close();
            }
            return res;
        }

    }
}
