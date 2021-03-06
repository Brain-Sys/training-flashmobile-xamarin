﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Settings;
using System;
using System.Net.Http;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.Requests;
using WhatsAppCrossMobile.Responses;

namespace WhatsAppCrossMobile.ViewModels
{
    public class RegisterViewModel : ApplicationViewModelBase
    {
        private string callerIdentifier;
        public string CallerIdentifier
        {
            get { return callerIdentifier; }
            set
            {
                callerIdentifier = value;
                base.RaisePropertyChanged();
            }
        }

        private string deviceIdentifier;
        public string DeviceIdentifier
        {
            get { return deviceIdentifier; }
            set
            {
                deviceIdentifier = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand RegisterNewDeviceCommand { get; set; }

        public RegisterViewModel()
        {
            this.RegisterNewDeviceCommand = new RelayCommand(RegisterNewDeviceCommandExecute);

            this.CallerIdentifier = CrossSettings.Current.GetValueOrDefault("CallerIdentifier", "Igor Damiani");
            this.DeviceIdentifier = CrossSettings.Current.GetValueOrDefault("DeviceIdentifier", "+393490101888");
        }

        private async void RegisterNewDeviceCommandExecute()
        {
            this.IsBusy = true;
            this.BusyMessage = "Registrazione in corso";

            var request = new AddDeviceRequest();
            request.CallerIdentifier = callerIdentifier;
            request.DeviceIdentifier = deviceIdentifier;

            string url = $"{base.BaseUrl}/Device/Add";

            var response = await base.MakeWebRequestAsync<AddDeviceRequest, AddDeviceResponse>(url, request);

            if (response != null)
            {
                IFile file = await base.RootFolder.CreateFileAsync("profile", PCLStorage.CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(string.Empty);

                CrossSettings.Current.AddOrUpdateValue("CallerIdentifier", this.CallerIdentifier);
                CrossSettings.Current.AddOrUpdateValue("DeviceIdentifier", this.DeviceIdentifier);
                Messenger.Default.Send<PromptMessage>(new PromptMessage("OK", "Registrazione completata!"));
                Messenger.Default.Send<NavigateMessage>(new NavigateMessage("MainPage"));
            }
            else
            {
                Messenger.Default.Send<PromptMessage>(new PromptMessage("OOPPSS", "Qualcosa è andato storto!"));
            }

            this.BusyMessage = string.Empty;
            this.IsBusy = false;
        }
    }
}