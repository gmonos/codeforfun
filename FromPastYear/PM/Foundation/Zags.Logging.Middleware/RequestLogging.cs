using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;


namespace Zags.Logging.Middleware
{
    using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;
    public class RequestLogging
    {

        private AppFunc _next;
        private readonly ILog Logger = LogManager.GetLogger<RequestLogging>();

        public RequestLogging(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);
            Logger.Info(string.Format(
              "Incoming request: {0}, {1}, {2}",
              owinContext.Request.Method,
              owinContext.Request.Path,
              owinContext.Request.Headers));
            await _next.Invoke(environment);
            Logger.Info(string.Format(
                  "Outgoing response: {0}, {1}",
                   owinContext.Response.StatusCode,
                   owinContext.Response.Headers));
        }
    }

}
