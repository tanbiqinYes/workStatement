using CCWOnlineAPIServer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CCWOnlineAPIServer.Filters
{
    //一次处理
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public ExceptionHandlingAttribute()
        {
            Mappings = new Dictionary<Type, HttpStatusCode>
            {
                {typeof (ArgumentNullException), HttpStatusCode.BadRequest},
                {typeof (ArgumentException), HttpStatusCode.BadRequest}
            };
        }

        public IDictionary<Type, HttpStatusCode> Mappings { get; private set; }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                var request = actionExecutedContext.Request;
                var exception = actionExecutedContext.Exception;

                if (actionExecutedContext.Exception is HttpException)
                {
                    var httpException = (HttpException)exception;
                    actionExecutedContext.Response =
                        request.CreateResponse((HttpStatusCode)httpException.GetHttpCode(), new Error { Message = exception.Message });
                }
                else if (Mappings.ContainsKey(exception.GetType()))
                {
                    var httpStatusCode = Mappings[exception.GetType()];
                    actionExecutedContext.Response =
                        request.CreateResponse(httpStatusCode, new Error { Message = exception.Message });
                }
                else
                {
                    actionExecutedContext.Response =
                        actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new Error { Message = exception.Message });
                }
            }
        }
    }
}