using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Skype.UCWA;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;

namespace TestClient
{
    class Program
    { 
        static void Main(string[] args)
        {
            UCWASample app = new UCWASample();
            app.Run();
            Console.Read();
        }
    }
}
