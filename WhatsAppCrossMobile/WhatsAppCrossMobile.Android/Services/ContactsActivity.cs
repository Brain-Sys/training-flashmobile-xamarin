using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using WhatsAppCrossMobile.Interfaces;
using WhatsAppCrossMobile.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ContactsActivity))]
namespace WhatsAppCrossMobile.Droid.Services
{
    [Activity(Label = "ContactsActivity")]
    public class ContactsActivity : Activity, IContactsService
    {
        public List<string> GetContacts()
        {
            var contacts = new List<string>();
            var uri = ContactsContract.Contacts.ContentUri;

            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName
            };

            var cursor = ManagedQuery(uri, projection, null, null, null);

            if (cursor.MoveToFirst())
            {
                do
                {
                    int index = cursor.GetColumnIndex(projection[1]);
                    string value = cursor.GetString(index);
                    contacts.Add(value);
                } while (cursor.MoveToNext());
            }

            return contacts;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}