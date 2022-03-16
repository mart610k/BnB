using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Enum
{
    public enum HttpStatusEnum
    {
        OK,
        PrimaryKeyFailed,
        UnspecifiedError,
        ClientFormatError,
        ForbiddenAction
    }
}