using Microsoft.Skype.UCWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace UCWAUWP.Models
{
    public class LocalContact
    {
        public Contact Contact { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
