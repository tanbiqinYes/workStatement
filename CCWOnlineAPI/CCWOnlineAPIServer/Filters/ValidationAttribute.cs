using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CCWOnlineAPIServer.Messages;


namespace CCWOnlineAPIServer.Filters
{
    //模型匹配验证过滤
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new Error
                    {
                        Name = e.Key,
                        Message = e.Value.Errors.First().ErrorMessage
                    }).ToArray();

                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}