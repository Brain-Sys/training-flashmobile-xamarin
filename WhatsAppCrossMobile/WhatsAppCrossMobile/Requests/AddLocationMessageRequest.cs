using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Requests
{
    public class AddLocationMessageRequest : AddMessageBaseRequest
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}