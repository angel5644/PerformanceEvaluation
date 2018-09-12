using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Enums
{
    public enum RequestType
    {
        Normal = 0,
        IsUnpaid = 1,
        UnEmergency = 2,
        Paternity = 3,
        Funeral = 4
    }
}