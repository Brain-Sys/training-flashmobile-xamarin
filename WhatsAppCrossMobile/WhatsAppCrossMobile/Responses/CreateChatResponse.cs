using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Responses
{
    public class CreateChatResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public string[] CallerIdentifiers { get; set; }
    }
}