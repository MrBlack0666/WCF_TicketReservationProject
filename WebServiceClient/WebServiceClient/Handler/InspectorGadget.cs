using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Windows;
using WebServiceClient;

namespace WebServiceClient.Handler
{
    class InspectorGadget : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var value = (Application.Current.MainWindow as MainWindow).password;

            request.Headers.Add(MessageHeader.CreateHeader("SomeHeader", "somenamespace", value));
            return null;
        }
    }
}
