using JulMar.Atapi;
using System;
using System.Net;
using System.Text;

namespace TapiIntegrationSample
{
    class Program
    {
        /// <summary>
        /// Opens an IBOS3 address by phone number. The number will be taken from a TAPI 
        /// event and will be passed to the IBOS3 remote control endpoint to open the address.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TapiManager tapiManager = new TapiManager("TapiCallMonitor.net");
            if (tapiManager.Initialize() == false)
            {
                Console.WriteLine("No Tapi devices found.");
                Console.ReadLine();
            }
            else
            {
                foreach (TapiLine line in tapiManager.Lines)
                {
                    try
                    {
                        line.NewCall += OnNewCall;
                        line.Monitor();
                    }
                    catch (TapiException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine("Waiting for calls");
                Console.ReadLine();
                tapiManager.Shutdown();
            }
        }

        private static void OnNewCall(object sender, NewCallEventArgs e)
        {
            TapiLine line = (TapiLine)sender;
            TapiCall call = e.Call;
            Console.WriteLine(call.CallerId);

            StringBuilder sb = new StringBuilder();
            sb.Append("http://127.0.0.1:15477/adresse-by-phone?msn=");
            sb.Append(call.CallerId);
            var result = new WebClient().DownloadString(sb.ToString());
            Console.WriteLine(result);
        }
    }
}
