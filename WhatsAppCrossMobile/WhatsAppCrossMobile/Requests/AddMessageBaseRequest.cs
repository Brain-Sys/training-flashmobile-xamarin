using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Requests
{
    public class AddMessageBaseRequest : RequestBase
    {
        public string Message { get; set; }
        public string ChatIdentifier { get; set; }
    }
}