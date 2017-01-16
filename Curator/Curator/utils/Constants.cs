using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    class Constants
    {
        public static string RestURL = "http://mill.com.co/ws/wiishper/process";
        public static int LIKE_PROD = 301;
        #region
        public static int SHOW_PRODS = 306;
        public static int ACCEPT_PROD = 307;
        public static int REJECT_PROD = 308;
        public static int OK = 0;
        public static int PENDING = 1;
        public static int REJECTING = 2;
        #endregion
    }
}
