using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Services
{
    static class Settings
    {
        public static string Host { get; set; }
        public static string Tenant { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string ClientId { get; set; }
        public static UCWAClient UCWAClient { get; set; }
    }
}
