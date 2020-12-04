using Microsoft.Owin.Hosting;
using Pumox.Server.Data;
using Pumox.Server.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumox.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["port"];
            var c = new dbPumox();
            try
            {
                using (WebApp.Start<Startup>("http://localhost:"+port))
                {
                    Console.WriteLine($"Listening on port {port}...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
