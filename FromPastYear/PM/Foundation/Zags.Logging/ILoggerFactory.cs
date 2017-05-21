using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging
{
    public interface ILoggerFactory
    {
        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}
