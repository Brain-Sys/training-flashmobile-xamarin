using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Models
{
    public class WhatsAppContact : ObservableObject
    {
        public string Initial
        {
            get
            {
                return this.FirstName.Substring(0, 1);
            }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.LastName))
                {
                    return this.FirstName;
                }
                else
                {
                    return string.Concat(this.FirstName, ", ", this.LastName);
                }
            }
        }

        private string id;
        public string ID
        {
            get { return id; }
            set { id = value;
                base.RaisePropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}