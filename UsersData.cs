using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HotelAppWPF
{
    internal class UsersData
    {
        public static Dictionary<string, string> Users { get; set; } = new Dictionary<string, string>
        {
            { "admin", "hotel123" }
        };
    }
}
