using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Messages
{
    public class PromptMessage : BaseMessage
    {
        public string Title { get; protected set; }
        public string Message { get; protected set; }

        public PromptMessage(string title = null, string message = null)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}