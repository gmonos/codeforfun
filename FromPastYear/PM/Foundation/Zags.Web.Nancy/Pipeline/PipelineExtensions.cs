using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Zags.Logging;
using Zags.Web.Http;

namespace Zags.Web.Nancy.Pipeline
{
    public static class PipelineExtensions
    {
        private static ILog Logger = LogManager.GetLogger("PipelineExtensions");

        public static void EnabledUnhandledExceptionHandling(this IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((c, ex) =>
            {
                Logger.Error("Technical Error ",ex);

                var response = new JsonResponse(new ErrorResponse
                {
                    Error = ex.GetType().Name,
                    ErrorDescription = ex.Message,
                    ErrorUri = ex.HelpLink
                }, new DefaultJsonSerializer());
                response.StatusCode = HttpStatusCode.InternalServerError;

                return response;
            });

        }
    }
}
