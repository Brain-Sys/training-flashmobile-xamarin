using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsAppCrossMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();
            Messenger.Default.Register<PromptMessage>(this, async (obj) => { await this.DisplayAlert(obj.Title, obj.Message, "OK"); });
            Messenger.Default.Register<SetFocusMessage>(this, (obj) =>
            {
                View control = this.FindByName<View>(obj.ControlName);
                control?.Focus();
            });
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var vm = this.Resources["viewmodel"] as ChatViewModel;
            vm?.SendMessageCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            Messenger.Default.Unregister<PromptMessage>(this);
            base.OnDisappearing();
        }
    }
}