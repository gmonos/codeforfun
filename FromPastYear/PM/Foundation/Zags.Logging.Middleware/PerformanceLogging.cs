using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Zags.Logging.Middleware
{
    using System.Diagnostics;

    public class PerformanceLogging: OwinMiddleware
    {

        private readonly ILog Logger = LogManager.GetLogger<PerformanceLogging>();

        public PerformanceLogging(OwinMiddleware next) :base(next)
        {
          
        }

        override public async Task Invoke(IOwinContext owinContext)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await Next.Invoke(owinContext);
            stopWatch.Stop();
         

            Logger.Info(string.Format("Request: {0} {1} executed in {2} ms",
                  owinContext.Request.Method, owinContext.Request.Path,
                  stopWatch.ElapsedMilliseconds));
        }
    }
}
