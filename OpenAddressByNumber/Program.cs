using System;
using System.IO;
using System.Net;
using System.Text;

namespace OpenAddressByNumber
{
    class Program
    {
        /// <summary>
        /// Opens an IBOS3 address by phone number. The first command line argument 
        /// will be passed to the IBOS3 remote control endpoint to open the address.
        /// </summary>
        /// <param name="args"></param>
     
        static void Main(string[] args)
        {   
            if (args.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("http://127.0.0.1:15477/adresse-by-phone?msn=");
                sb.Append(args[0]);
                var result = new WebClient().DownloadString(sb.ToString());
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
