using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Responses
{
    public class AddDeviceResponse : BaseResponse
    {
        public Guid ID { get; set; }
        public string CallerIdentifier { get; set; }
        public string DeviceIdentifier { get; set; }
        public DateTime CreateAt { get; set; }
    }
}