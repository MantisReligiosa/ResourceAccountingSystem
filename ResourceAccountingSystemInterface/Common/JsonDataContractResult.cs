using Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace ResourceAccountingSystemInterface.Common
{
    class JsonDataContractResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context.IsNull())
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, Http.Get, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Get is not allowed");
            }
            var responce = context.HttpContext.Response;
            responce.ContentType = String.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            if (!ContentEncoding.IsNull())
            {
                responce.ContentEncoding = ContentEncoding;
            }
            if (!Data.IsNull())
            {
                using (var writer = new StreamWriter(responce.OutputStream))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, Data);
                    jsonWriter.Flush();
                }
            }
        }
    }
}