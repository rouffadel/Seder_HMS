using System;
using System.IO;
using System.Net;



namespace SMSConfig
{
    public class SMSCAPI
    {
        private WebProxy objProxy1 = null;
        public string SendSMS(string User, string password, string Mobile_Number, string Message)
        {

            string stringpost = null;
            stringpost = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message;

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;
                if (System.Configuration.ConfigurationSettings.AppSettings["SMSStatus"].ToString() == "on")
                {
                    objWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice.asp");
                    objWebRequest.Method = "POST";

                    if ((objProxy1 != null))
                    {
                        objWebRequest.Proxy = objProxy1;
                    }

                    objWebRequest.ContentType = "application/x-www-form-urlencoded";

                    objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                    objStreamWriter.Write(stringpost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();

                    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                    stringResult = objStreamReader.ReadToEnd();

                    objStreamReader.Close();

                }
                return (stringResult);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {

                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }


        }

        private void StreamReader(Stream stream)
        {
            throw new NotImplementedException();
        }

        public string SendSMS(string User, string password, string Mobile_Number, string Message, string Mtype)
        {

            System.Object stringpost = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype;


            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {

                string stringResult = null;
                if (System.Configuration.ConfigurationSettings.AppSettings["SMSStatus"].ToString() == "on")
                {
                    objWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice.asp?");
                    objWebRequest.Method = "POST";

                    if ((objProxy1 != null))
                    {
                        objWebRequest.Proxy = objProxy1;
                    }

                    objWebRequest.ContentType = "application/x-www-form-urlencoded";

                    objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                    objStreamWriter.Write(stringpost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();

                    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                    stringResult = objStreamReader.ReadToEnd();

                    objStreamReader.Close();
                }
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            finally
            {

                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }

        public string SendSMS(string User, string password, string Mobile_Number, string Message, string Mtype, string DR)
        {

            System.Object stringpost = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype + "&DR=" + DR;

       

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;
                if (System.Configuration.ConfigurationSettings.AppSettings["SMSStatus"].ToString() == "on")
                {
                    objWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice.asp?");
                    objWebRequest.Method = "POST";

                    if ((objProxy1 != null))
                    {
                        objWebRequest.Proxy = objProxy1;
                    }

                    objWebRequest.ContentType = "application/x-www-form-urlencoded";

                    objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                    objStreamWriter.Write(stringpost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();

                    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                    stringResult = objStreamReader.ReadToEnd();

                    objStreamReader.Close();
                }
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            finally
            {

                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }

    }
}





