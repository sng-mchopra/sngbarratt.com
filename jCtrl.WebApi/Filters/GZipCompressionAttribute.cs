using System.Net.Http;
using System.Web.Http.Filters;

namespace jCtrl.WebApi.Filters
{
    public class GZipCompressionAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {

            if(actContext.Response != null) { 

                var content = actContext.Response.Content;
                var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
                var zlibbedContent = bytes == null ? new byte[0] : jCtrl.Services.Core.Utils.Helpers.CompressionHelper.GZipByte(bytes);

                actContext.Response.Content = new ByteArrayContent(zlibbedContent);
                actContext.Response.Content.Headers.Remove("Content-Type");
                actContext.Response.Content.Headers.Add("Content-encoding", "gzip");
                actContext.Response.Content.Headers.Add("Content-Type", "application/json");
            }

            base.OnActionExecuted(actContext);
        }
    }
}