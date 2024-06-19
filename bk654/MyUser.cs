using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bk654
{
    internal static class MyUser
    {
        public static int UserId { get; set; }

        static MyUser()
        {
            UserId = Properties.Settings.Default.id;
        }
    }
}
