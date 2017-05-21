using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging.Middleware
{
    using Microsoft.Owin;
    using System.Diagnostics;
    using AppFunc = System.Func<IDictionary<string, object>, Task>;
    public class CorrelationToken
    {
        private AppFunc _next;
        private readonly ILog Logger = LogManager.GetLogger<PerformanceLogging>();

        public CorrelationToken(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            Guid correlationToken;

            var owinContext = new OwinContext(environment);

            if (!(owinContext.Request.Headers["Correlation-Token"] != null && Guid.TryParse(owinContext.Request.Headers["Correlation-Token"],
                    out correlationToken)))
                correlationToken = Guid.NewGuid();

            owinContext.Set("correlationToken", correlationToken.ToString());
            
            using (Logger.PushProperty("CorrelationToken", correlationToken))
                await _next.Invoke(environment);
        }
    }
}
