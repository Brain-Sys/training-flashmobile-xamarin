using GalaSoft.MvvmLight;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.ViewModels
{
    public class ApplicationViewModelBase : ViewModelBase
    {
        // protected string BaseUrl { get; private set; } = "http://localhost:5000/api";
        protected string BaseUrl { get; private set; } = "http://flashmobileservices.azurewebsites.net/api";

        protected IFolder RootFolder { get; private set; } = FileSystem.Current.LocalStorage;
        protected HttpClient Client { get; private set; } = new HttpClient();
        public object Parameter { get; set; }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
                base.RaisePropertyChanged();
            }
        }

        private string busyMessage;
        public string BusyMessage
        {
            get { return busyMessage; }
            set { busyMessage = value;
                base.RaisePropertyChanged();
                base.RaisePropertyChanged(nameof(IsBusyMessageVisible));
            }
        }

        public bool IsBusyMessageVisible
        {
            get
            {
                return !string.IsNullOrEmpty(this.BusyMessage);
            }
        }

        public virtual void Init()
        {

        }
    }
}