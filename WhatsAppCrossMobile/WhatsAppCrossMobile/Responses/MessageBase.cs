using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Responses
{
    public class MessageBase : DocumentBase<string>
    {
        private bool read;
        public bool Read
        {
            get { return read; }
            set
            {
                read = value;
                base.RaisePropertyChanged();
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                base.RaisePropertyChanged();
            }
        }

        private string chatIdentifier;
        public string ChatIdentifier
        {
            get { return chatIdentifier; }
            set
            {
                chatIdentifier = value;
                base.RaisePropertyChanged();
            }
        }
    }
}