using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Services
{
    internal static class UniqueIdGenerator
    {
        static int id = 0;

        static public int GenerateNewId()
        {
            return id++;
        }
    }
}
