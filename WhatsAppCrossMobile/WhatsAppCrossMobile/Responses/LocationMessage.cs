using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Responses
{
    public class LocationMessage : MessageBase
    {
        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value;
                base.RaisePropertyChanged();
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value;
                base.RaisePropertyChanged();
            }
        }
    }
}