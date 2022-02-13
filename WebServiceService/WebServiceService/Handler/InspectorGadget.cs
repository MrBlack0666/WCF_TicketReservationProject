using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using WebServiceService.PasswordAttributes;

namespace WebServiceService.Handler
{
    public class InspectorGadget : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {

            string actionName = request.Headers.Action.Substring(request.Headers.Action.LastIndexOf('/') + 1);
            var methodInfo = instanceContext.Host.Description.ServiceType.GetMethod(actionName);
            if (methodInfo != null)
            {
                var customAttributes = methodInfo.GetCustomAttributes(false);
                var customAttribute = (PasswordAttribute)customAttributes.FirstOrDefault(x => x.GetType().Equals(typeof(PasswordAttribute)));
                if (customAttribute != null)
                {
                    string password = "";
                    if(request.Headers.FindHeader("SomeHeader", "somenamespace") >= 0)
                    {
                        password = request.Headers.GetHeader<string>("SomeHeader", "somenamespace");
                    }
                    if (password != customAttribute.Password)
                    {
                        throw new FaultException("Podano złe hasło", new FaultCode("authorizationFault"));
                    }
                }
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}