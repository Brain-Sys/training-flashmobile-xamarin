using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Messages
{
    public class NavigateMessage : BaseMessage
    {
        public string DestionationPage { get; private set; }
        public object Parameter { get; set; }

        public NavigateMessage(string destionationPage)
        {
            this.DestionationPage = destionationPage;
        }
    }
}