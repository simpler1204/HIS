using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Class
{
    interface IReceiveMessage
    {
         void MessageFromWinccOA(string val);
    }
}
