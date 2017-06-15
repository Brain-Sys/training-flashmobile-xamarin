using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Settings;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.Models;
using WhatsAppCrossMobile.Requests;
using WhatsAppCrossMobile.Responses;
using mvvm = GalaSoft.MvvmLight.Messaging;

namespace WhatsAppCrossMobile.ViewModels
{
    public class ChatViewModel : ApplicationViewModelBase
    {
        public string ChatId { get; private set; }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
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
                this.SendTextMessageCommand.RaiseCanExecuteChanged();
            }
        }

        private WhatsAppContact contact;
        public WhatsAppContact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                base.RaisePropertyChanged();
            }
        }

        private bool sendingMessage;
        public bool SendingMessage
        {
            get { return sendingMessage; }
            set { sendingMessage = value;
                base.RaisePropertyChanged();
            }
        }

        public ObservableCollection<MessageBase> Messages { get; set; }

        public RelayCommand SendTextMessageCommand { get; set; }

        public ChatViewModel()
        {
            this.SendTextMessageCommand = new RelayCommand(SendTextMessageCommandExecute, SendTextMessageCommandCanExecute);
            this.Messages = new ObservableCollection<MessageBase>();
        }

        private bool SendTextMessageCommandCanExecute()
        {
            return !string.IsNullOrEmpty(this.Text);
        }

        private async void SendTextMessageCommandExecute()
        {
            this.SendingMessage = true;
            HttpResponseMessage response = null;

            var request = new AddMessageBaseRequest();
            request.CallerIdentifier = CrossSettings.Current.GetValueOrDefault("CallerIdentifier", string.Empty);
            request.DeviceIdentifier = CrossSettings.Current.GetValueOrDefault("DeviceIdentifier", string.Empty);
            request.ChatIdentifier = this.ChatId;
            request.Message = this.Text;

            string url = $"{base.BaseUrl}/Message/AddTextMessage";

            try
            {
                var formData = request.GetFormData();
                response = await base.Client.PostAsync(url, formData);
            }
            catch (Exception)
            {

            }

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TextMessage>(content);
                this.Messages.Add(result);
                this.Text = string.Empty;
                mvvm.Messenger.Default.Send<SetFocusMessage>(new SetFocusMessage() { ControlName = "First" });
            }
            else
            {
                mvvm.Messenger.Default.Send<PromptMessage>(new PromptMessage("OOPPSS", "Qualcosa è andato storto!"));
            }

            this.SendingMessage = false;
        }

        public async override void Init()
        {
            this.Contact = this.Parameter as WhatsAppContact;
            this.Title = $"Chat con {this.Contact.DisplayName}";

            var chatExists = await base.RootFolder.CheckExistsAsync(this.Contact.ID);

            if (chatExists == ExistenceCheckResult.NotFound)
            {
                this.createNewChat();
            }
            else
            {
                this.reloadChat();
            }

            mvvm.Messenger.Default.Send<SetFocusMessage>(new SetFocusMessage() { ControlName = "First" });
        }

        private async void reloadChat()
        {
            this.IsBusy = true;
            this.BusyMessage = "Recupero chat in corso";

            HttpResponseMessage response = null;

            IFile chatFile = await base.RootFolder.GetFileAsync(this.Contact.ID);
            this.ChatId = await chatFile.ReadAllTextAsync();
            string url = $"{base.BaseUrl}/Message/GetMessages?chatIdentifier={this.ChatId}";

            try
            {
                response = await base.Client.GetAsync(url);
            }
            catch (Exception)
            {

            }

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<TextMessage[]>(content);
                this.Messages = new ObservableCollection<MessageBase>(messages);
                base.RaisePropertyChanged(nameof(Messages));
            }
            else
            {
                mvvm.Messenger.Default.Send<PromptMessage>(new PromptMessage("OOPPSS", "Qualcosa è andato storto!"));
                mvvm.Messenger.Default.Send<NavigateBackMessage>(new NavigateBackMessage());
            }

            this.BusyMessage = string.Empty;
            this.IsBusy = false;
        }

        private async void createNewChat()
        {
            this.IsBusy = true;
            this.BusyMessage = "Creazione chat in corso";

            HttpResponseMessage response = null;
            var request = new CreateChatRequest();
            request.CallerIdentifier = CrossSettings.Current.GetValueOrDefault("CallerIdentifier", string.Empty);
            request.DeviceIdentifier = CrossSettings.Current.GetValueOrDefault("DeviceIdentifier", string.Empty);
            request.CallerIndentifiers = new string[] { this.Contact.ID };

            string url = $"{base.BaseUrl}/Chat/Create";

            try
            {
                var formData = request.GetFormData();
                response = await base.Client.PostAsync(url, formData);
            }
            catch (Exception)
            {

            }

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateChatResponse>(content);
                this.ChatId = result.ID.ToString();
                var filename = this.Contact.ID.ToString();
                IFile file = await base.RootFolder.CreateFileAsync(filename, PCLStorage.CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(result.ID.ToString());
            }
            else
            {
                mvvm.Messenger.Default.Send<PromptMessage>(new PromptMessage("OOPPSS", "Qualcosa è andato storto!"));
                mvvm.Messenger.Default.Send<NavigateBackMessage>(new NavigateBackMessage());
            }

            this.BusyMessage = string.Empty;
            this.IsBusy = false;
        }
    }
}