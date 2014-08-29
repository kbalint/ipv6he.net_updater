using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace iv6HEnet_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            string userid=""; // login userid | tunnelbroker.net -> Account Info -> Account Name
            string password=""; // tunnel ID from tunnelbroker.net -> Advanced tab -> Update key
            string hostname=""; // tunnel ID from tunnelbroker.net -> IPv6 tunnel tab -> Tunnel ID

            string url = string.Format("https://ipv4.tunnelbroker.net/nic/update?username={0}&password={1}&hostname={2}", userid, password, hostname);

            WebResponse response=null;
            StreamReader readStream=null;

            WebRequest wr = WebRequest.CreateHttp(url);
            wr.Credentials = new NetworkCredential(userid, password);

            try
            {
                response = wr.GetResponse();
                Stream receiveStream = response.GetResponseStream();

                readStream = new StreamReader(receiveStream, Encoding.UTF8);
                Console.WriteLine(readStream.ReadToEnd());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message); // usually 401 if you misstyped the login parameters.
            }
            finally
            {
                if (response!=null) response.Close();
                if (readStream!=null) readStream.Close();
            }
        }
    }
}
