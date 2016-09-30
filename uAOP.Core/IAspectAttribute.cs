using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uAOP.Core
{
    public interface IAspectAttribute
    {
        int Priority { get; }
    }
}
