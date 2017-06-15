using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCrossMobile.Messages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsAppCrossMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Messenger.Default.Register<PromptMessage>(this, async (obj) => { await this.DisplayAlert(obj.Title, obj.Message, "OK"); });
        }

        protected override void OnDisappearing()
        {
            Messenger.Default.Unregister<PromptMessage>(this);
            base.OnDisappearing();
        }
    }
}