using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging
{
    public enum LogLevel
    {
        /// <summary>
        /// Debug level messages.
        /// </summary>
        Debug,

        /// <summary>
        /// Information level messages.
        /// </summary>
        Info,

        /// <summary>
        /// Warning level messages.
        /// </summary>
        Warn,

        /// <summary>
        /// Error level messages.
        /// </summary>
        Error,

        /// <summary>
        /// Fatal level messages.
        /// </summary>
        Fatal
    }
}
