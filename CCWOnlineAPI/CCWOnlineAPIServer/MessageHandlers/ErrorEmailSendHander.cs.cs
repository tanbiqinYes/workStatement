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
    public class ErrorEmailSendHander : DelegatingHandler
    {

        public ErrorEmailSendHander(DelegatingHandler innerChannel)
            : base(innerChannel)
        {
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //该方法中统一返回异常，并发送错误邮件，读取错误邮件配置
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