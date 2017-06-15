using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Messages
{
    public class QuestionMessage : PromptMessage
    {
        public Action Yes { get; set; }
        public Action No { get; set; }

        public QuestionMessage(string title = null, string message = null)
        {
            base.Title = title;
            base.Message = message;
        }
    }
}