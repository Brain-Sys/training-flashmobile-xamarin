using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using WhatsAppWpf.Models;

namespace WhatsAppWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly string callerIdentifier = "caller";
        private readonly string deviceIdentifier = "device";
        private readonly string baseUrl = "http://localhost:5000/api";
        private HttpClient client = new HttpClient();
        public RelayCommand TestCommand { get; set; }
        public RelayCommand RegisterNewDeviceCommand { get; set; }
        public RelayCommand CreateChatCommand { get; set; }
        public RelayCommand DeleteChatCommand { get; set; }

        public MainViewModel()
        {
            this.RegisterNewDeviceCommand = new RelayCommand(RegisterNewDeviceCommandExecute);
            this.TestCommand = new RelayCommand(TestCommandExecute);
            this.CreateChatCommand = new RelayCommand(CreateChatCommandExecute);
            this.DeleteChatCommand = new RelayCommand(DeleteChatCommandExecute);
        }

        private async void DeleteChatCommandExecute()
        {
            DeleteChatRequest request = new DeleteChatRequest();
            request.CallerIdentifier = callerIdentifier;
            request.DeviceIdentifier = deviceIdentifier;
            request.ChatIdentifier = "410ef468-c9f4-4175-b8bf-9dc551bab21f";

            string url = $"{baseUrl}/Chat/Delete?ChatIdentifier={request.ChatIdentifier}";
            //var response = await client.PostAsync(url, request.GetFormData());
            //var response = await client.PostAsJsonAsync<DeleteChatRequest>(url, request);
            var response = await client.DeleteAsync(url);

            //if (response != null && response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    //var result = JsonConvert.DeserializeObject<CreateChatResponse>(content);
            //}
        }

        private async void CreateChatCommandExecute()
        {
            CreateChatRequest request = new CreateChatRequest();
            request.CallerIdentifier = callerIdentifier;
            request.DeviceIdentifier = deviceIdentifier;
            request.CallerIndentifiers = new string[] { "1", "2", "4" };

            string url = $"{baseUrl}/Chat/Create";
            var response = await client.PostAsync(url, request.GetFormData());

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateChatResponse>(content);
            }
        }

        private async void RegisterNewDeviceCommandExecute()
        {
            AddDeviceRequest request = new AddDeviceRequest();
            request.CallerIdentifier = callerIdentifier;
            request.DeviceIdentifier = deviceIdentifier;

            string url = $"{baseUrl}/Device/Add";
            var response = await client.PostAsync(url, request.GetFormData());

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AddDeviceResponse>(content);
            }
        }

        private async void TestCommandExecute()
        {
            string url = $"{baseUrl}/Device/Get?id=abc";
            var result = await client.GetAsync(url);
        }
    }
}