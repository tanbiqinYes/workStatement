using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CCWOnlineAPIServer.MessageHandlers
{
    //对所有的请求记录日志，比如操作日志
    public class TraceMessageHandler : DelegatingHandler
    {
        public TraceMessageHandler(DelegatingHandler innerChannel)
            : base(innerChannel)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request.Method == HttpMethod.Trace)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(
                    () =>
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.OK);
                        response.Content = new StringContent(request.ToString(), Encoding.UTF8, "message/http");
                        return response;
                    });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}