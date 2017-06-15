using GalaSoft.MvvmLight.Messaging;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsAppCrossMobile.Messages;
using WhatsAppCrossMobile.ViewModels;
using Xamarin.Forms;

namespace WhatsAppCrossMobile
{
    public partial class App : Application
    {
        public App()
        {
            Messenger.Default.Register<NavigateMessage>(this, navigate);

            InitializeComponent();
            MainPage = new NavigationPage();
        }

        private async void navigate(NavigateMessage obj)
        {
            Type t = Type.GetType("WhatsAppCrossMobile." + obj.DestionationPage);

            if (t != null)
            {
                Page page = Activator.CreateInstance(t) as Page;

                if (page != null)
                {
                    if (obj.Parameter != null)
                    {
                        var vm = page.Resources["viewmodel"] as ApplicationViewModelBase;
                        vm.Parameter = obj.Parameter;
                        vm.Init();
                    }

                    await this.MainPage.Navigation.PushAsync(page, true);
                }
            }
        }

        protected async override void OnStart()
        {
            try
            {
                ExistenceCheckResult profileExists = ExistenceCheckResult.NotFound;
                var rootFolder = FileSystem.Current.LocalStorage;

                switch(Xamarin.Forms.Device.RuntimePlatform)
                {
                    case "Android":
                        {
                            profileExists = rootFolder.CheckExistsAsync("profile").Result;
                            break;
                        }
                    case "Windows":
                        {
                            profileExists = await rootFolder.CheckExistsAsync("profile");
                            break;
                        }
                }
                
                Page destinationPage = null;

                if (profileExists == ExistenceCheckResult.FileExists)
                {
                    destinationPage = new MainPage();
                }
                else
                {
                    destinationPage = new RegisterPage();
                }

                await this.MainPage.Navigation.PushAsync(destinationPage);
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
