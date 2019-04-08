// ZBytes.
// 20190408, Honda, Copy from ZLib.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data.Linq; // Binary

namespace ZLib
{
    public static class ZByte
    {
        public static Boolean ZCompare(this Binary b1, Binary b2)
        {
            return b1.Equals(b2);
        }

    }
}
