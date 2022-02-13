using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace WebServiceService.Handler
{
    public class InspectorGadgetEndpointBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(InspectorGadgetBehavior);

        protected override object CreateBehavior()
        {
            return new InspectorGadgetBehavior();
        }
    }
}