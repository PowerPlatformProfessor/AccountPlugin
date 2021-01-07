using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPlugin.Enum
{
    public enum StageName
    {
        PreValidation = 10,
        PreOperation = 20,
        MainOperation = 30,
        PostOperation = 40,
    }
}
