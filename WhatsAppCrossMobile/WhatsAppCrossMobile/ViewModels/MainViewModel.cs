using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PCLStorage;
using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.Models;

namespace WhatsAppCrossMobile.ViewModels
{
    public class MainViewModel : ApplicationViewModelBase
    {
        public ObservableCollection<IGrouping<string, WhatsAppContact>> Contacts { get; set; }
        public RelayCommand DeleteDeviceCommand { get; set; }
        public RelayCommand<WhatsAppContact> StartSingleChatCommand { get; set; }

        public MainViewModel()
        {
            this.DeleteDeviceCommand = new RelayCommand(DeleteDeviceCommandExecute);
            this.StartSingleChatCommand = new RelayCommand<WhatsAppContact>(StartSingleChatCommandExecute);
            loadContacts();
        }

        private void StartSingleChatCommandExecute(WhatsAppContact contact)
        {
            if (contact == null) return;

            var msg = new NavigateMessage("ChatPage");
            msg.Parameter = contact;
            Messenger.Default.Send<NavigateMessage>(msg);
        }

        private void DeleteDeviceCommandExecute()
        {
            var question = new QuestionMessage("Conferma", "Disconnettere questo device?");
            question.Yes = async () =>
            {
                this.IsBusy = true;

                var exists = await base.RootFolder.CheckExistsAsync("profile");

                if (exists == ExistenceCheckResult.FileExists)
                {
                    IFile file = await base.RootFolder.GetFileAsync("profile");
                    await file.DeleteAsync();
                }

                Messenger.Default.Send<NavigateMessage>(new NavigateMessage("RegisterPage"));

                this.IsBusy = false;
            };

            Messenger.Default.Send<QuestionMessage>(question);
        }

        private async void loadContacts()
        {
            var perm = await CrossContacts.Current.RequestPermission();

            if (perm)
            {
                CrossContacts.Current.PreferContactAggregation = false;

                await Task.Run(() =>
                {
                    if (CrossContacts.Current.Contacts == null)
                        return;

                    var contacts = CrossContacts.Current.Contacts.ToList();
                    var projection = contacts
                    .Where(c => !string.IsNullOrEmpty(c.FirstName))
                    .OrderBy(c => c.LastName)
                    .Select(c => new WhatsAppContact()
                    {
                        ID = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    });

                    List<IGrouping<string, WhatsAppContact>> groupedContacts = projection.GroupBy(p => p.Initial)
                    .OrderBy(p => p.Key).ToList();

                    this.Contacts = new ObservableCollection<IGrouping<string, WhatsAppContact>>(groupedContacts);
                    base.RaisePropertyChanged(nameof(Contacts));
                });
            }
        }
    }
}