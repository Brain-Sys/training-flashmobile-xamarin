using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.Models;
using WhatsAppCrossMobile.ViewModels;
using Xamarin.Forms;

namespace WhatsAppCrossMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register<QuestionMessage>(this, questionToTheUser);
        }

        private async void questionToTheUser(QuestionMessage obj)
        {
            var answer = await this.DisplayAlert(obj.Title, obj.Message, "Sì", "No");

            if (answer)
            {
                obj.Yes?.Invoke();
            }
            else
            {
                obj.No?.Invoke();
            }
        }

        protected override void OnDisappearing()
        {
            Messenger.Default.Unregister<QuestionMessage>(this);
            base.OnDisappearing();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = this.Resources["viewmodel"] as MainViewModel;
            vm.StartSingleChatCommand.Execute(e.Item);
        }
    }
}
